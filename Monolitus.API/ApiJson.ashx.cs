﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Converters;
using rfl = System.Reflection;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using Cinar.Database;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Monolitus.DTO;
using Monolitus.API.Entity;
using Monolitus.DTO.Response;
using Monolitus.DTO.Enums;
using Monolitus.DTO.Request;
using System.Web.SessionState;
using Monolitus.DTO.EntityInfo;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;

namespace Monolitus.API
{
    public class BaseAPI : IHttpHandler, IRequiresSessionState
    {
        protected HttpContext context;
        private string apiType = "json";

        public string ClientIp { get; set; }
        public Session Session { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            HttpContext.Current.Items["api"] = this;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
                return settings;
            });


            this.context = context;
            if (context.Request["apiType"] == "xml") apiType = "xml";

            string clientIPAddress = getIPAddress();

            string data = "", method = "", clientName = "";
            object req = null;
            rfl.MethodInfo mi = null;
            try
            {
                //if (!ConfigurationManager.AppSettings["allowedIPs"].Contains(clientIPAddress))
                //    throw new APIException("Access denied for " + context.Request.UserHostAddress);

                if (apiType == "xml")
                    context.Response.ContentType = "application/xml";
                else
                    context.Response.ContentType = "application/json";

                method = context.Request["method"];
                if (string.IsNullOrWhiteSpace(method))
                    throw new APIException("Service request method needed");
                mi = this.GetType().GetMethod(method);

                if (mi == null)
                    throw new APIException("There is no service method with the name " + method);

                if (mi.GetParameters().Length != 1)
                    throw new APIException("A service request method should have only one parameter");

                data = context.Request["data"];
                if (string.IsNullOrWhiteSpace(data))
                    throw new APIException("Service request data needed");
                data = HttpUtility.UrlDecode(data);
                Type t = getServiceRequestType(mi.GetParameters()[0].ParameterType);

                req = deserialize(data, t);

                var client = req.GetMemberValue("Client");
                clientName = client == null ? "" : client.ToString();

                object sessionId = req.GetMemberValue("SessionId");
                if (sessionId != null && !string.IsNullOrWhiteSpace(sessionId.ToString()))
                    this.Session = Provider.Database.Read<Session>("Id={0}", sessionId);

                if (this.Session == null)
                    this.Session = new Session();


                if (!string.IsNullOrWhiteSpace(this.Session.UserId))
                    Provider.CurrentUser = this.Session.GetUser();


                object res = mi.Invoke(this, new[] { req.GetMemberValue("Data") });

                this.Session.LastAccess = DateTime.Now;
                this.Session.SerializedParams = this.Session.Params.Serialize();
                this.Session.Save();

                t = getServiceResponseType(mi.ReturnType);
                object serviceResponse = Activator.CreateInstance(t);
                serviceResponse.SetMemberValue("Data", res);
                serviceResponse.SetMemberValue("IsSuccessful", true);
                serviceResponse.SetMemberValue("ClientIPAddress", clientIPAddress);
                serviceResponse.SetMemberValue("SessionId", this.Session.Id);

                sw.Stop();

                serviceResponse.SetMemberValue("ServerProcessTime", sw.ElapsedMilliseconds);

                context.Response.Write(serialize(serviceResponse, apiType));
            }
            catch (Exception ex)
            {
                sw.Stop();

                if (ex.InnerException is APIException)
                {
                    var exInner = ex.InnerException as APIException;
                    context.Response.Write(serialize(new ServiceResponse<object>
                        {
                            Data = (mi != null && mi.ReturnType == typeof (bool)) ? (object) false : null,
                            IsSuccessful = false,
                            ErrorMessage = exInner.Message,
                            ErrorType = (int) exInner.ErrorType,
                            ErrorCode = (int) exInner.ErrorCode,
                            ClientIPAddress = clientIPAddress,
                            ServerProcessTime = sw.ElapsedMilliseconds
                        }, apiType));
                }
                else
                {
                    context.Response.Write(serialize(new ServiceResponse<object>
                        {
                            Data = (mi != null && mi.ReturnType == typeof(bool)) ? (object)false : null,
                            IsSuccessful = false,
                            ErrorMessage = ex.ToStringBetter(),
                            ErrorCode = 500,
                            ClientIPAddress = clientIPAddress,
                            ServerProcessTime = sw.ElapsedMilliseconds
                        }, apiType));
                }
            }
        }

        #region utility

        private string serialize(object obj, string apiType)
        {
            if (apiType == "json")
                return JsonConvert.SerializeObject(obj, Formatting.Indented);

            return obj.Serialize();
        }
        private object deserialize(string data, Type type)
        {
            if (apiType == "json")
                return JsonConvert.DeserializeObject(data, type);
            return data.Deserialize(type);
        }

        private Type getServiceRequestType(Type item)
        {
            var t = typeof(ServiceRequest<>);
            Type[] typeArgs = { item };
            return t.MakeGenericType(typeArgs);
        }
        private Type getServiceResponseType(Type item)
        {
            var t = typeof(ServiceResponse<>);
            Type[] typeArgs = { item };
            return t.MakeGenericType(typeArgs);
        }

        public List<rfl.MethodInfo> GetServiceMethods()
        {
            return this.GetType().GetMethods(rfl.BindingFlags.DeclaredOnly | rfl.BindingFlags.Public | rfl.BindingFlags.Instance).ToList();
        }
        public string GetServiceMethodDescription(rfl.MethodInfo mi)
        {
            DescriptionAttribute desc = mi.GetAttribute<DescriptionAttribute>();

            string description = "<table><tr><td colspan=\"2\"><i>" + (desc == null ? "No description." : desc.Description) + "</i></td></tr>";
            description += "<tr><td colspan=\"2\">&nbsp;</td></tr>";

            foreach (rfl.PropertyInfo pi in mi.GetParameters()[0].ParameterType.GetProperties())
            {
                DescriptionAttribute desc2 = pi.GetAttribute<DescriptionAttribute>();
                description += string.Format("<tr><td><b>{0}</b></td><td>{1}</td></tr>", pi.Name, (desc2 == null ? "" : desc2.Description));
            }

            description += "</table>";

            return description;
        }
        public string GetServiceMethodRequestSample(rfl.MethodInfo mi, string apiType)
        {
            if (mi == null)
                return "No such service method";

            if (mi.GetParameters().Length != 1)
                return "A service request method should have only one parameter";

            Type t = getServiceRequestType(mi.GetParameters()[0].ParameterType);
            object req = Activator.CreateInstance(t);
            req.SetMemberValue("ApiKey", "SAMPLE_API_KEY");
            object data;
            if (mi.GetParameters()[0].ParameterType == typeof(string))
                data = "";
            else
                data = Activator.CreateInstance(mi.GetParameters()[0].ParameterType);
            req.SetMemberValue("Data", data);

            foreach (rfl.PropertyInfo pi in data.GetType().GetProperties(rfl.BindingFlags.DeclaredOnly | rfl.BindingFlags.Public | rfl.BindingFlags.Instance))
                if (!(pi.PropertyType.IsPrimitive || pi.PropertyType == typeof(string)) && pi.GetSetMethod() != null)
                    pi.SetValue(data, pi.PropertyType.IsArray ? Array.CreateInstance(pi.PropertyType.GetElementType(), 0) : Activator.CreateInstance(pi.PropertyType), null);

            return serialize(req, apiType);
        }

        private string getIPAddress()
        {
            HttpContext context = HttpContext.Current;

            if (!string.IsNullOrWhiteSpace(context.Request.ServerVariables["HTTP_CLIENT_IP"]))
                return context.Request.ServerVariables["HTTP_CLIENT_IP"];

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.UserHostAddress;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        protected object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
        #endregion
    }

    public class ApiJson : BaseAPI
    {
        #region Common

        public List<IdName> GetEnumList(string enumName)
        {
            var enumType = typeof (UserTypes).Assembly.GetType("Monolitus.DTO.Enums." + enumName);
            if(enumType==null)
                throw new APIException("Enum not found. Did you put it in enums folder?");

            var res = new List<IdName>();
            foreach (var id in Enum.GetNames(enumType))
                res.Add(new IdName(){Id = id, Name = id.PascalCaseWords().StringJoin(" ")});

            return res;
        }
        public bool SendTestMail(string email)
        {
            Provider.SendMail(email, "Your Email", "Test mail", "Test");
            return true;
        }

        #endregion

        #region Users

        public UserInfo GetUserInfo(ReqEmpty req)
        {
            var res = Provider.Database.Read<User>("Id = {0}", Session.UserId).ToEntityInfo<UserInfo>();
            return res;
        }
        public bool SaveProfileInfo(ProfileInfo req)
        {
            User u = Session.GetUser();
            req.CopyPropertiesWithSameName(u);
            u.Id = Session.UserId;
            u.Save();

            return true;
        }
        public ProfileInfo GetProfileInfo(ReqEmpty req)
        {
            var res = new ProfileInfo();
            Session.GetUser().CopyPropertiesWithSameName(res);
            return res;
        }

        public bool ChangeMemberPassword(ReqChangeMemberPassword req)
        {
            if (req.NewPassword != req.NewPasswordAgain)
                throw new APIException("Passwords not same", ErrorTypes.ValidationError);

            User member = Provider.Database.Read<User>("Id={0} AND Password={1} AND IsDeleted=0", Session.UserId,
                                               System.Utility.MD5(req.OldPassword).ToUpperInvariant().Substring(0, 16));
            if (member == null)
                throw new APIException("No such user");

            try
            {
                member.Password = System.Utility.MD5(req.NewPassword).ToUpperInvariant().Substring(0, 16);
                member.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeMemberEmail(string email)
        {
            if (!(email ?? "").IsEmail())
                throw new APIException("Please enter a valid email address", ErrorTypes.ValidationError);

            var member = Provider.CurrentUser;
            if (member == null || member.IsAnonim())
                throw new APIException("No such user", ErrorTypes.ValidationError);

            try
            {
                member.NewEmail = email;
                member.Keyword = Utility.CreatePassword(16);
                member.Save();

                member.SendConfirmationCode();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeMemberPhoneNumber(string phoneNo)
        {
            if (phoneNo.IsEmpty())
                throw new APIException("Please enter a valid phone number", ErrorTypes.ValidationError);

            phoneNo = phoneNo.TrimStart('0').Replace(" ", "");
            if (!new Regex("\\d{10}").Match(phoneNo).Success)
                throw new Exception("Phone number must be 10 digits. (örn: 8181234567)");

            var member = Provider.CurrentUser;
            if (member == null || member.IsAnonim())
                throw new APIException("No such user", ErrorTypes.ValidationError);

            try
            {
                member.NewPhoneNumber = phoneNo;
                member.Keyword = Utility.CreatePassword(5);
                member.Save();
                

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ConfirmEmailChange(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new APIException("Code required", ErrorTypes.ValidationError);

            var member = Provider.Database.Read<User>("Keyword = {0}", keyword);
            if (member == null)
                throw new APIException("No such user", ErrorTypes.ValidationError);

            if (!string.IsNullOrWhiteSpace(member.NewEmail))
                member.Email = member.NewEmail;
            member.NewEmail = "";
            member.IsDeleted = false;
            member.Keyword = "";
            member.EmailValidated = true;
            member.Save();

            Provider.CurrentUser = member;

            return true;
        }
        public bool ConfirmPhoneNumberChange(string keyword)
        {
            if (Provider.CurrentUser.Id.IsEmpty())
                throw new APIException("Access denied", ErrorTypes.ValidationError);

            if (keyword.IsEmpty())
                throw new APIException("Code required", ErrorTypes.ValidationError);

            var member = Provider.Database.Read<User>("Id={0} AND Keyword = {1}", Provider.CurrentUser.Id, keyword);
            if (member == null)
                throw new APIException("Wrong code", ErrorTypes.ValidationError);

            if (!string.IsNullOrWhiteSpace(member.NewPhoneNumber))
                member.PhoneCell = member.NewPhoneNumber;
            member.NewPhoneNumber = "";
            member.IsDeleted = false;
            member.Keyword = "";
            member.PhoneCellValidated = true;
            member.Save();

            Provider.CurrentUser = member;

            return true;
        }

        public ResLogin SignUp(ReqSignUp req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || !req.Email.IsEmail())
                throw new APIException("Invalid email address", ErrorTypes.ValidationError);
            if ((req.Password ?? "").Length < 6)
                throw new APIException("Password length cannot be less than 6 characters");
            if (req.Password != req.Password2)
                throw new APIException("Passwords not same", ErrorTypes.ValidationError);

            if (!string.IsNullOrWhiteSpace(Session.UserId))
                throw new APIException("You are already a member", ErrorTypes.ValidationError,
                                       ErrorCodes.ExistingMemberCannotSignUp);

            User user = Provider.Database.Read<User>("Email={0}", req.Email);
            if (user != null)
                throw new APIException("Used email address. Please log in.", ErrorTypes.ValidationError, ErrorCodes.ExistingMemberCannotSignUp);
            else
                user = new User();

            req.CopyPropertiesWithSameName(user);
            user.Keyword = Utility.CreatePassword(12);
            user.Password = Utility.MD5(req.Password).ToUpperInvariant().Substring(0, 16);
            user.UserType = UserTypes.User;
            user.LastLoginDate = DateTime.Now;
            user.IsDeleted = true;
            user.Save();

            user.SendConfirmationCode();
            user.SendWelcomeMessage();

            return doLoginForUser(user);;
        }
        public ResLogin LoginWithFacebook(ReqLoginWithFacebook req)
        {
            try
            {
                if (!req.Email.IsEmail())
                    throw new APIException("Invalid email address", ErrorTypes.ValidationError);

                User user = Provider.Database.Read<User>("FacebookId={0}", req.FacebookId);
                if (user != null)
                {
                    return doLoginForUser(user);
                }

                user = Provider.Database.Read<User>("Email={0}", req.Email);
                if (user != null)
                {
                    if (string.IsNullOrWhiteSpace(user.Avatar)) user.Avatar = req.Avatar;
                    user.FacebookId = req.FacebookId;
                    if (string.IsNullOrWhiteSpace(user.Name)) user.Name = req.Name;
                    if (string.IsNullOrWhiteSpace(user.Surname)) user.Surname = req.Surname;
                    if (user.Gender == 0) user.Gender = req.Gender == "M" ? Cinsiyet.Erkek : Cinsiyet.Bayan;
                    user.Save();

                    return doLoginForUser(user);
                }

                user = new User
                {
                    Email = req.Email,
                    EmailValidated = true,
                    UserType = UserTypes.User,
                    IsDeleted = false,             // facebook'tan gelen üye default onaylıdır (no need for email confirmation)
                    Avatar = req.Avatar,
                    FacebookId = req.FacebookId,
                    Name = req.Name,
                    Surname = req.Surname,
                    Gender = req.Gender == "M" ? Cinsiyet.Erkek : Cinsiyet.Bayan,
                    LastLoginDate = DateTime.Now,
                    Keyword = Utility.CreatePassword(16)
                };
                user.Save();

                return doLoginForUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<div style=\"display:none\">" + ex.StackTrace + "</div>");
            }
        }
        public bool SendPasswordRecoveryMessage(string email)
        {
            if (!email.IsEmail())
                throw new APIException("Invalid email address", ErrorTypes.ValidationError);

            var member = Provider.Database.Read<User>("Email = {0}", email);
            if (member != null)
                member.SendPasswordRecoveryMessage();

            return true;
        }
        public bool SendConfirmationMessage(ReqEmpty req)
        {
            Provider.CurrentUser.Keyword = Utility.CreatePassword(16);
            Provider.CurrentUser.Save();

            Provider.CurrentUser.SendConfirmationCode();
            return true;
        }
        public bool ChangeForgottenPassword(ReqChangeForgottenPassword req)
        {
            if (string.IsNullOrWhiteSpace(req.Keyword))
                throw new APIException("Code required", ErrorTypes.ValidationError);
            if ((req.NewPassword ?? "").Length < 6)
                throw new APIException("Password length cannot be less than 6 characters", ErrorTypes.ValidationError);
            if (req.NewPassword != req.NewPasswordAgain)
                throw new APIException("Passwords not same", ErrorTypes.ValidationError);

            var member = Provider.Database.Read<User>("Keyword = {0}", req.Keyword);
            if (member == null)
                throw new APIException("No such user", ErrorTypes.ValidationError);


            try
            {
                member.Password = System.Utility.MD5(req.NewPassword).ToUpperInvariant().Substring(0, 16);
                member.IsDeleted = false;
                member.Keyword = "";
                member.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Login

        public ResLogin Login(ReqLogin req)
        {
            User user = Provider.Database.Read<User>("Email={0} AND Password={1} AND IsDeleted=0", req.Email,
                                                           System.Utility.MD5(req.Password).ToUpperInvariant().Substring(0, 16));

            if (user == null)
                throw new APIException(Provider.TR("Invalid email or password"));

            return doLoginForUser(user);
        }
        private ResLogin doLoginForUser(User user)
        {
            var res = new ResLogin()
            {
                User = null,
                SessionId = string.Empty
            };
            Provider.CurrentUser = user;

            res.User = new UserInfo();
            user.CopyPropertiesWithSameName(res.User);

            user.LastLoginDate = DateTime.Now;
            user.Save();

            this.Session.UserId = user.Id;
            this.Session.LoginDate = DateTime.Now;
            this.Session.LastAccess = DateTime.Now;
            this.Session.Save();

            res.SessionId = this.Session.Id;

            return res;
        }
        public bool Logout(ReqEmpty req)
        {
            this.Session.Delete();
            this.Session.Id = "";
            return true;
        }

        #endregion

        #region monolitus

        public bool SaveMessage(MessageInfo req)
        {
            if (!req.Email.IsEmpty() && !req.Email.IsEmail())
                throw new APIException("Email adresi hatalı");

            if (req.UserId.IsEmpty())
                req.UserId = Provider.CurrentUser.Id;

            var m = new Message();
            req.CopyPropertiesWithSameName(m);
            m.Save();

            Provider.SendMail("Visiter message: "+req.Subject, req.MessageText);

            return true;
        }

        #endregion
    }
}