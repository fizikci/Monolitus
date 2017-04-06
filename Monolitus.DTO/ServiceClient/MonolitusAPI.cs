﻿using Facebook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Monolitus.DTO.EntityInfo;
using Monolitus.DTO.Request;
using Monolitus.DTO.Response;

namespace Monolitus.DTO.ServiceClient
{
    public class MonolitusAPI : BaseAPI, IAPIProvider
    {
        #region Common

        public string GetAPIVersion()
        {
            return "1.0";
        }
        public List<IdName> GetEnumList(string enumName)
        {
            return Call<List<IdName>, string>(enumName, MethodBase.GetCurrentMethod().Name);
        }
        public bool SendTestMail(string email) {
            return Call<bool, string>(email, MethodBase.GetCurrentMethod().Name);
        
        }
        #endregion

        #region Users
        public UserInfo GetUserInfo(ReqEmpty req)
        {
            var res = Call<UserInfo, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
            HttpContext.Current.Session["UserInfo"] = res;
            return res;
        }
        public bool SaveProfileInfo(ProfileInfo req)
        {
            return Call<bool, ProfileInfo>(req, MethodBase.GetCurrentMethod().Name);
        }
        public ProfileInfo GetProfileInfo(ReqEmpty req)
        {
            return Call<ProfileInfo, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
        }
        
        public bool ChangeMemberPassword(ReqChangeMemberPassword req)
        {
            return Call<bool, ReqChangeMemberPassword>(req, MethodBase.GetCurrentMethod().Name);
        }
        public bool ChangeMemberEmail(string email)
        {
            return Call<bool, string>(email, MethodBase.GetCurrentMethod().Name);
        }
        public bool ChangeMemberPhoneNumber(string phoneNo) 
        {
            var res = Call<bool, string>(phoneNo, MethodBase.GetCurrentMethod().Name);
            return res;
        }
        public bool ConfirmEmailChange(string keyword)
        {
            return Call<bool, string>(keyword, MethodBase.GetCurrentMethod().Name);
        }
        public bool ConfirmPhoneNumberChange(string keyword) 
        {
            var res = Call<bool, string>(keyword, MethodBase.GetCurrentMethod().Name);
            if (res)
                this.User.PhoneCellValidated = true;
            return res;
        }
        public ResLogin SignUp(ReqSignUp req)
        {
            var res = Call<ResLogin, ReqSignUp>(req, MethodBase.GetCurrentMethod().Name);
            HttpContext.Current.Session["UserInfo"] = res.User;
            return res;
        }
        public ResLogin LoginWithFacebook(ReqLoginWithFacebook req)
        {
            HttpContext.Current.Session["AccessToken"] = req.AccessToken;

            var client = new FacebookClient(req.AccessToken);
            dynamic result = client.Get("me", new { fields = "id,name,email,gender,first_name,last_name,picture" });

            req = new ReqLoginWithFacebook()
            {
                FacebookId = result.id,
                Name = result.first_name,
                Surname = result.last_name,
                Avatar = result.picture.data.url,
                Email = result.email,
                Nick = result.username,
                Gender = result.gender == "male" ? "M" : "F"
            };

            var res = Call<ResLogin, ReqLoginWithFacebook>(req, MethodBase.GetCurrentMethod().Name);
            this.SessionId = res.SessionId;
            HttpContext.Current.Session["UserInfo"] = res.User;
            return res;
        }
        public bool SendPasswordRecoveryMessage(string email)
        {
            return Call<bool, string>(email, MethodBase.GetCurrentMethod().Name);
        }
        public bool SendConfirmationMessage(ReqEmpty req)
        {
            return Call<bool, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
        }
        public bool ChangeForgottenPassword(ReqChangeForgottenPassword req)
        {
            return Call<bool, ReqChangeForgottenPassword>(req, MethodBase.GetCurrentMethod().Name);
        }
        #endregion

        #region Login
        public ResLogin Login(ReqLogin req)
        {
            var res = Call<ResLogin, ReqLogin>(req, MethodBase.GetCurrentMethod().Name);
            HttpContext.Current.Session["UserInfo"] = res.User;
            return res;
        }
        public bool Logout(ReqEmpty req)
        {
            var res = Call<bool, ReqEmpty>(req, MethodBase.GetCurrentMethod().Name);
            this.SessionId = null;
            HttpContext.Current.Session["UserInfo"] = null;
            return res;
        }
        #endregion

        #region monolitus


        public bool SaveMessage(MessageInfo req)
        {
            return Call<bool, MessageInfo>(req, MethodBase.GetCurrentMethod().Name);
        }

        #endregion


        protected override string GetServiceURL()
        {
            return ConfigurationManager.AppSettings["MonolitusServiceURL"];
        }

        public object CallWebAPIMethod(string method)
        {

            if (string.IsNullOrWhiteSpace(method))
                throw new Exception("Service request method needed");

            MethodInfo mi = this.GetType().GetMethod(method);

            if (mi == null)
                throw new Exception("There is no service method with the name " + method);

            Type t = mi.GetParameters()[0].ParameterType;

            object param = Deserialize(HttpContext.Current.Request.Form["data"], t);

            //if (t == typeof (string))
            //    param = HttpContext.Current.Request.Form[mi.GetParameters()[0].Name];
            //else if (t.IsValueType)
            //    param = HttpContext.Current.Request.Form[mi.GetParameters()[0].Name].ChangeType(t);
            //else
            //{
            //    param = Activator.CreateInstance(t);
            //    (param as ISetFieldsByPostData).SetFieldsByPostData();
            //}

            object res = mi.Invoke(this, new object[] {param});

            return res;

        }

        #region Member, Order Provider for web applications
        public UserInfo User
        {
            get
            {
                if (HttpContext.Current.Session["UserInfo"] != null)
                    return (UserInfo)HttpContext.Current.Session["UserInfo"];


                return new UserInfo() { Id = "", Name = "Anonim" };
            }
            set { HttpContext.Current.Session["UserInfo"] = value; }
        }
        #endregion
    }
}