using System;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace Monolitus.API.Client.Request
{
    public class BaseRequest : ISetFieldsByPostData
    {
        public virtual void SetFieldsByPostData()
        {
            var postData = HttpContext.Current.Request.Form;

            for (int i = 0; i < postData.Count; i++)
            {
                PropertyInfo pi = this.GetType().GetProperty(postData.GetKey(i));
                if (pi == null || pi.GetSetMethod() == null) continue;

                string strVal = postData[i];

                if (pi.PropertyType == typeof(bool))
                {
                    if (strVal.ToLower() == "1") strVal = "True";
                    if (strVal.ToLower() == "0") strVal = "False";
                    if (strVal.ToLower() != "true") strVal = "False";
                }

                object val = null;
                try
                {
                    if (pi.PropertyType.IsEnum)
                        val = Enum.Parse(pi.PropertyType, strVal);
                    else
                        val = Convert.ChangeType(strVal, pi.PropertyType, CultureInfo.CurrentCulture);
                }
                catch
                {
                    throw new Exception(string.Format("The field {0} cannot have {1} as value.", this.GetType().Name + "." + pi.Name, strVal));
                }

                pi.SetValue(this, val, null);
            }
        }
    }
}
