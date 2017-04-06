using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Monolitus.DTO.ServiceClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Configuration;

namespace Monolitus.DTO.Handlers
{
    public class APIBridgeHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = ConfigurationManager.AppSettings["developmentMode"]=="true" ? "application/json" : "plain/text";
            try
            {
                JsonConvert.DefaultSettings = (() =>
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
                    return settings;
                });

                string method = HttpContext.Current.Request["method"];
                object result = new MonolitusAPI().CallWebAPIMethod(method);

                HttpContext.Current.Response.Write(Serialize(new AjaxResponse() { Data = result }));
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(Serialize(new AjaxResponse() { Data = null, ErrorMessage = ex.ToStringBetter(), IsError = true }));
            }
        }

        protected string Serialize(object obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);

            if (ConfigurationManager.AppSettings["developmentMode"] == "true")
                return jsonStr;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonStr);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        protected object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class AjaxResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
