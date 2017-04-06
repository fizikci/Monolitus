using System.Configuration;
using System.Reflection;
using System.Web;
using System;
using System.Collections.Generic;
using Monolitus.API.Client.Request;
using Monolitus.API.Client.ServiceClient;

namespace Monolitus.API.Client
{
    public class MonolitusAPI : BaseAPI
    {
        public ResLogin Login(ReqLogin req)
        {
            return Call<ResLogin, ReqLogin>(req, MethodBase.GetCurrentMethod().Name);
        }
        public bool Logout(ReqEmpty req)
        {
            return Call<bool, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
        }

        public List<ListViewUserCompanyInfo> GetUserList(ReqGetUserList req)
        {
            return Call<List<ListViewUserCompanyInfo>, ReqGetUserList>(req, MethodBase.GetCurrentMethod().Name);
        }

        public CompanyAllInfo GetCompanyAllInfo(ReqEmpty req)
        {
            return Call<CompanyAllInfo, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
        }

        public bool UpdateCompanyAllInfo(CompanyAllInfo req)
        {
            return Call<bool, CompanyAllInfo>(req, MethodBase.GetCurrentMethod().Name);
        }

        public List<ListViewUserCompanyInfo> GetUserCancelList(ReqGetUserCancelList req)
        {
            return Call<List<ListViewUserCompanyInfo>, ReqGetUserCancelList>(req, MethodBase.GetCurrentMethod().Name);
        }

        public override string GetServiceURL()
        {
            var url = ConfigurationManager.AppSettings["MonolitusApiUrl"];
            if(string.IsNullOrWhiteSpace(url))
                throw new APIException("Add Monolitus parameter to your config file");
            return url;
        }

        public object CallWebAPIMethod(string method)
        {

            if (string.IsNullOrWhiteSpace(method))
                throw new Exception("Service request method needed");

            MethodInfo mi = this.GetType().GetMethod(method);

            if (mi == null)
                throw new Exception("There is no service method with the name " + method);

            Type t = mi.GetParameters()[0].ParameterType;

            object param = null;
            if (t == typeof(string))
                param = HttpContext.Current.Request.Form[mi.GetParameters()[0].Name];
            else if (t.IsValueType)
                param = Convert.ChangeType(HttpContext.Current.Request.Form[mi.GetParameters()[0].Name], t);
            else
            {
                param = Activator.CreateInstance(t);
                (param as ISetFieldsByPostData).SetFieldsByPostData();
            }

            object res = mi.Invoke(this, new object[] { param });

            return res;

        }
    }
}
