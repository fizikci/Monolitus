using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Monolitus.API.Staff
{
    public class BasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Request.RawUrl.Contains("Login.aspx"))
                return;

            if (Provider.CurrentUser.IsAnonim())
            {
                Response.Redirect("/Staff/Login.aspx?RedirectUrl="+Server.UrlEncode(Request.RawUrl), true);
                return;
            }
        }
    }
}