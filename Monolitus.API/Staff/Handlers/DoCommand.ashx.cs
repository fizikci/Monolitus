﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Monolitus.API.Entity.Common;
using Monolitus.API.Entity;

namespace Monolitus.API.Staff.Handlers
{
    /// <summary>
    /// Summary description for Report
    /// </summary>
    public class DoCommand : IHttpHandler, IRequiresSessionState
    {
        HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            switch (context.Request["method"])
            {
                case "exportUsersToExcel":
                    {
                        exportUsersToExcel();
                        break;
                    }
                case "uploadAvatar":
                    {
                        uploadAvatar();
                        break;
                    }
                default:
                    {
                        sendErrorMessage("Henüz " + context.Request["method"] + " isimli metod yazılmadı.");
                        break;
                    }
            }
        }
        protected void sendErrorMessage(string message)
        {
            context.Response.Write("ERR: " + message);
        }
        protected void sendErrorMessage(Exception ex)
        {
            sendErrorMessage(ex.ToStringBetter());
        }


        private void exportUsersToExcel()
        {
            context.Response.Clear();

            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/csv";

            DataTable dt = null;
            if (Provider.CurrentUser.UserType == DTO.Enums.UserTypes.Admin)
            {
                dt = Provider.Database.GetDataTable("SELECT u.Name AS Ad, u.Surname AS Soyad, u.Email, '' AS Sifre FROM User u ORDER BY 1,2,3");
                context.Response.AppendHeader("content-disposition", "attachment; filename=AllUsers.csv");
            }

            context.Response.Write("\"Ad\",\"Soyad\",\"Email\",\"Sifre\""+Environment.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                context.Response.Write("\""+dr.ItemArray.Select(o=>(string)o).ToList().StringJoin("\",\"")+"\"");
                context.Response.Write(Environment.NewLine);
            }
            context.Response.Flush();
            context.Response.End();
        }
        

        private void uploadAvatar()
        {
            try
            {
                string uid = context.Request["uid"];
                if (uid.IsEmpty())
                    throw new Exception("uid?");

                var user = Provider.Database.Read<User>("Id = {0}", uid);
                if (user == null)
                    throw new Exception("Kullanıcı bulunamadı");

                string path = context.Server.MapPath("/Medya/Avatars");

                string fileName = uid + Path.GetExtension(context.Request.Files[0].FileName);
                string imgPath = Path.Combine(path, fileName);
                try
                {
                    // eğer dosya resim ise resize edelim
                    Image bmp = Image.FromStream(context.Request.Files[0].InputStream);
                    if (bmp.Width > 200)
                    {
                        Image bmp2 = bmp.ScaleImage(200, 0);
                        bmp2.SaveImage(imgPath, 100);
                    }
                    else
                        context.Request.Files[0].SaveAs(imgPath);
                }
                catch
                {
                    context.Request.Files[0].SaveAs(imgPath);
                }

                user.Avatar = Path.GetFileName(imgPath);
                user.Save();

                context.Response.Write(@"<script>window.parent.postMessage('"+Path.GetFileName(imgPath)+"', '*');</script>");
            }
            catch (Exception ex)
            {
                context.Response.Write(@"<script>window.parent.postMessage('ERR:', '*');</script>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }

    
}

 	
	
		
	
	
		
	
		
