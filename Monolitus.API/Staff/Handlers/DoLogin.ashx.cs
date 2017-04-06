using Monolitus.API.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Monolitus.API.Staff.Handlers
{
    /// <summary>
    /// Summary description for Report
    /// </summary>
    public class DoLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["logout"] == "1")
            {
                Provider.CurrentUser = null;
                context.Response.Redirect("/Staff/Login.aspx", true);
                return;
            }

            //do login and set session(user and roles)
            User user = (User)Provider.Database.Read(typeof(User), "Email={0} and Password={1} and IsDeleted=0", context.Request["Email"], Utility.MD5(context.Request["Passwd"]).ToUpperInvariant().Substring(0, 16));

            if (user != null)
            {
                // login başarılı, RedirectURL sayfasına gönderelim.
                Provider.CurrentUser = user;

                string redirect = context.Request["RedirectUrl"];
                if (string.IsNullOrWhiteSpace(redirect))
                    context.Response.Redirect("/Staff/Default.aspx");
                else
                    context.Response.Redirect(redirect);
            }
            else
            {
                // login başarıSIZ, login formunun olduğu sayfaya geri gönderelim
                context.Session["loginError"] = "Email veya şifre geçersiz.";
                context.Response.Redirect("/Staff/Login.aspx");
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