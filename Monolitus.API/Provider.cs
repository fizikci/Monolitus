using System.Collections;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Monolitus.API.Entity;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Net;
using SmsApi;
using SmsApi.Types;
using Monolitus.DTO;

namespace Monolitus.API
{
    public static class Provider
    {
        private static Database _db;

        public static Database Database
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["db"] == null)
                    {
                        Database db = GetNewDatabaseInstance();
                        db.DefaultCommandTimeout = 1000;
                        db.CreateTablesAutomatically = true;
                        HttpContext.Current.Items["db"] = db;
                    }
                    return (Database)HttpContext.Current.Items["db"];
                }

                if (_db == null)
                    _db = GetNewDatabaseInstance();
                return _db;
            }
        }

        public static Database GetNewDatabaseInstance()
        {
            return new Database(ConfigurationManager.AppSettings["dbConnStr"], DatabaseProvider.MySQL, null, false, false);
        }

        public static ApiJson Api
        {
            get
            {
                return HttpContext.Current.Items["api"] as ApiJson;
            }
        }



        public static List<T> ReadListWithCache<T>(this IDatabase db) where T : IDatabaseEntity
        {
            if (HttpContext.Current.Cache[typeof(T).FullName] == null)
                HttpContext.Current.Cache.Insert(typeof(T).FullName, db.ReadList<T>(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0));

            return (List<T>)HttpContext.Current.Cache[typeof(T).FullName];
        }

        public static User CurrentUser {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                    return (User)HttpContext.Current.Session["User"];


                return new User() { Id = "", Name = "Anonim" };
            }
            set { HttpContext.Current.Session["User"] = value; }
        }

        public static List<TEntityInfo> ToEntityInfo<TEntityInfo>(this IList list) where TEntityInfo : new()
        {
            var res = new List<TEntityInfo>();
            foreach (var entity in list)
            {
                var info = new TEntityInfo();
                entity.CopyPropertiesWithSameName(info);
                res.Add(info);
            }
            return res;
        }
        public static TEntityInfo ToEntityInfo<TEntityInfo>(this BaseEntity entity) where TEntityInfo : new()
        {
            if (entity == null) 
                return default(TEntityInfo);

            var res = new TEntityInfo();
            entity.CopyPropertiesWithSameName(res);
            return res;
        }

        // Provider.TR("{0} records of total {1}", recCount, total);
        public static string TR(string text, params object[] parameters)
        {
            return string.Format(text, parameters);
        }


        public static T ReadEntityWithRequestCache<T>(string id) where T : BaseEntity, new()
        {
            var key = typeof (T).Name + "_" + id;
            if (!HttpContext.Current.Items.Contains(key))
            {
                var e = Provider.Database.Read<T>("Id={0}", id);
                if(e==null)
                    return new T();
                HttpContext.Current.Items.Add(key, e);
            }

            return (T)HttpContext.Current.Items[key];
        }

        public static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();

            try
            {

                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path, Encoding.UTF8))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn dataColumn = new DataColumn(column);
                        dataColumn.AllowDBNull = true;
                        csvData.Columns.Add(dataColumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

        public static void SendMail(string subject, string message)
        {
            SendMail(null, null, null, null, subject, message);
        }
        public static void SendMail(string toEmail, string toDisplayName, string subject, string message)
        {
            SendMail(null, null, toEmail, toDisplayName, subject, message);
        }
        public static void SendMail(string fromEmail, string fromDisplayName, string toEmail, string toDisplayName, string subject, string message)
        {
            string mailHost = "mail.monolit.us";
            int mailPort = 25;
            string mailUserName = "info@monolit.us";
            string mailFrom = "info@monolit.us";
            string mailPassword = "sdfsdfsdf";
            string mailFromDisplayName = "Monolitus";

            message = MailSablon.Replace("#{Body}", message);

            Utility.SendMail(fromEmail ?? mailFrom, fromDisplayName ?? mailFromDisplayName, toEmail ?? mailFrom, toDisplayName ?? mailFromDisplayName, subject, message, mailHost, mailPort, mailUserName, mailPassword, mailFrom);                
        }

        public static string MailSablon = @"

    <div align=""center"">
        <table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""600"" style=""width:450.0pt;border:solid #dddddd 1.0pt"">
            <tbody>
                <tr>
                    <td valign=""top"" style=""border:none;background:#ffc000;padding:0cm 0cm 0cm 0cm"">
                        <p class=""MsoNormal"">
                            <span>
                                <img width=""285"" height=""149"" src=""http://www.monolit.us/UserFiles/logo_be.png"" class=""CToWUd"">
                            </span>
                        </p>
                    </td>
                </tr>
                <tr style=""height:266.15pt"">
                    <td valign=""top"" style=""border:none;background:#fdfdfd;padding:0cm 0cm 0cm 0cm;height:266.15pt"">
                        <div align=""center"">
                            <table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""600"" style=""width:450.0pt"">
                                <tbody>
                                    <tr style=""height:237.35pt"">
                                        <td valign=""top"" style=""background:#ffc000;padding:7.5pt 7.5pt 7.5pt 7.5pt;height:237.35pt"">
                                            <p class=""MsoNormal""><u></u>&nbsp;<u></u></p>
                                            <table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""width:100.0%"">
                                                <tbody>
                                                    <tr>
                                                        <td valign=""top"" style=""padding:7.5pt 7.5pt 7.5pt 7.5pt"">
                                                            #{Body}
                                                            <p class=""MsoNormal"" align=""center"" style=""text-align:center""><u></u>&nbsp;<u></u></p><p class=""MsoNormal"" align=""center"" style=""text-align:center""><span style=""font-size:14.0pt;color:#1f4e79"">monolit.us</span><b><span style=""font-size:12.0pt""><u></u><u></u></span></b></p>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <p class=""MsoNormal""><span style=""font-size:12.0pt;""><u></u><u></u></span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign=""top"" style=""background:#ffc000;padding:7.5pt 7.5pt 7.5pt 7.5pt"">
                                            <p class=""MsoNormal""><u></u>&nbsp;<u></u></p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>


";

    }
}