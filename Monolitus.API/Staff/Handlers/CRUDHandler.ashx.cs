using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Cinar.Database;
using Monolitus.API;
using Monolitus.API.Entity;
using Monolitus.DTO.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Monolitus.API.Staff.Handlers
{
    public class CRUDHandler : IHttpHandler, IRequiresSessionState
    {
        protected HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            string method = "";
            try
            {
                context.Response.ContentType = "application/json";

                JsonConvert.DefaultSettings = (() =>
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
                    return settings;
                });

                method = context.Request["method"];

                var entityName = context.Request["entityName"];
                foreach (var t in typeof(BaseEntity).Assembly.GetTypes())
                    if (t.FullName.StartsWith("Monolitus.API.Entity.") && t.Name == entityName)
                    {
                        T = t;
                        break;
                    }

                if (T == null)
                    throw new Exception("Entity not found: " + entityName);

                if (string.IsNullOrWhiteSpace(method))
                    throw new Exception("Ajax method needed");

                MethodInfo mi = this.GetType().GetMethod(method);

                if (mi == null)
                    throw new Exception("There is no ajax method with the name " + method);

                object[] paramValues = new object[mi.GetParameters().Length];

                if (mi.GetParameters().Length > 0)
                {
                    for (int i = 0; i < mi.GetParameters().Length; i++)
                    {
                        ParameterInfo pi = mi.GetParameters()[i];
                        try
                        {
                            paramValues[i] = context.Request[pi.Name].ChangeType(pi.ParameterType);
                        }
                        catch
                        {
                            throw new Exception("Error converting " + context.Request[pi.Name] + " TO " + pi.ParameterType);
                        }
                    }
                }

                var res = mi.Invoke(this, paramValues);

                context.Response.Write(Serialize(new AjaxResponse()
                {
                    isError = false,
                    errorMessage = "",
                    data = res
                }));
            }
            catch (Exception ex)
            {
                context.Response.Write(Serialize(new AjaxResponse()
                {
                    isError = true,
                    errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    data = null
                }));
            }
        }

        #region utility
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


        private Type T;

        public object GetById(string id)
        {
            string[] fullNameParts = T.FullName.Split('.');
            fullNameParts[3] = "View" + T.Name;

            // read from database
            var viewType = Type.GetType(fullNameParts.StringJoin("."));

            return Provider.Database.Read(viewType ?? T, "Id={0}", id);
        }

        public ResList GetList(int pageSize, int pageNo)
        {
            var fExp = readFilterExpression(pageSize, pageNo);
            modifyFilterByUserRights(fExp);

            // is there an avilable view for T
            string[] fullNameParts = T.FullName.Split('.');
            fullNameParts[3] = "ListView" + T.Name;

            // read from database
            IList list = null;
            var viewType = Type.GetType(fullNameParts.StringJoin("."));
            list = Provider.Database.ReadList(viewType ?? T, fExp);

            var count = Provider.Database.ReadCount(viewType ?? T, fExp);

            return new ResList { 
                list = list,
                count = count
            };
        }

        public IList GetIdNameList(int pageSize, int pageNo)
        {
            var fExp = readFilterExpression(pageSize, pageNo);
            modifyFilterByUserRights(fExp);

            return Provider.Database.ReadList(T, fExp)
               .Select(c => new IdName { Id = ((BaseEntity)c).Id, Name = c.ToString() })
               .ToList();
        }

        public bool DeleteById(string id)
        {
            var entity = (BaseEntity)Provider.Database.Read(T, "Id={0}", id);
            entity.Delete();
            return true;
        }
        public bool UndeleteById(string id)
        {
            var entity = (BaseEntity)Provider.Database.Read(T, "Id={0}", id);
            entity.IsDeleted = false;
            entity.Save();
            return true;
        }

        public bool Save(string id)
        {
            try
            {
                var entity = (BaseEntity)Provider.Database.Read(T, "Id={0}", id);
                if (entity == null) entity = (BaseEntity)Activator.CreateInstance(T);
                entity.SetFieldsByPostData(context.Request.Form);

                entity.Save();

                if (!string.IsNullOrWhiteSpace(context.Request["redirectToList"]))
                    context.Response.Redirect("/Staff/List" + T.Name + ".aspx", true);
                else
                    context.Response.Redirect(context.Request.UrlReferrer + "&saved=1");

                return true;
            }
            catch
            {
                context.Server.Transfer("/Staff/Edit" + T.Name + ".aspx");
                return false;
            }
        }

        public BaseEntity SaveWithAjax()
        {
            var entity = (BaseEntity)Provider.Database.Read(T, "Id={0}", context.Request.Form["Id"]);
            if (entity == null)
            {
                entity = (BaseEntity)Activator.CreateInstance(T);
                entity.IsDeleted = false;
            }
            entity.SetFieldsByPostData(context.Request.Form);

            entity.Save();

            return entity;
        }

        public List<IdName> GetEnumList(string enumName)
        {
            return new ApiJson().GetEnumList(enumName);
        }

        #region private implementation
        private static FilterExpression readFilterExpression(int pageSize, int pageNo)
        {
            // where
            FilterExpression fExp = null;
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.Form["Where"]))
                fExp = FilterExpression.Parse(HttpContext.Current.Request.Form["Where"]);
            if (fExp == null)
                fExp = new FilterExpression() { PageNo = pageNo, PageSize = pageSize };
            else
            {
                fExp.PageNo = pageNo;
                fExp.PageSize = pageSize;
            }

            // order by
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.Form["OrderBy"]))
            {
                var orderBy = HttpContext.Current.Request.Form["OrderBy"].Split(' ');
                fExp.OrderBy(orderBy[0]);
                if (orderBy.Length > 1 && orderBy[1].ToLowerInvariant() == "desc")
                    fExp.Desc();
            }
            return fExp;
        }
        private void modifyFilterByUserRights(FilterExpression fExp) {
            if (Provider.CurrentUser.UserType != DTO.Enums.UserTypes.Admin)
                return; //*** bu filtre sadece temsilcilere uygulanıyor

            //if (T == typeof(Ulke)) {
            //    if (Provider.CurrentUser.UserType == DTO.Enums.UserTypes.Admin);
            //    else if (Provider.CurrentUser.IsUserUlkeTemsilcisi() != "" || Provider.CurrentUser.IsUserKurumTemsilcisi() != "")
            //        fExp = fExp.And("Id", CriteriaTypes.Eq, Provider.CurrentUser.UlkeId);
            //    else
            //        fExp = fExp.And("Id", CriteriaTypes.Eq, "Hicbiri");
            //}
            //else if (T == typeof(Kurum)) {
            //    if (Provider.CurrentUser.UserType == DTO.Enums.UserTypes.Admin);
            //    else if(Provider.CurrentUser.IsUserUlkeTemsilcisi()!="")
            //        fExp = fExp.And("UlkeId", CriteriaTypes.Eq, Provider.CurrentUser.IsUserUlkeTemsilcisi());
            //    else if (Provider.CurrentUser.IsUserKurumTemsilcisi() != "")
            //        fExp = fExp.And("Id", CriteriaTypes.Eq, Provider.CurrentUser.IsUserKurumTemsilcisi());
            //    else 
            //        fExp = fExp.And("Id", CriteriaTypes.Eq, "Hicbiri");
            //}
        }
        #endregion
    }

    public class AjaxResponse
    {
        public bool isError { get; set; }
        public string errorMessage { get; set; }
        public object data { get; set; }
    }
    public class ResList
    {
        public int count { get; set; }
        public IList list { get; set; }
    }
}