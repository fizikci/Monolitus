using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Monolitus.API.Staff.Handlers
{
    /// <summary>
    /// Summary description for Report
    /// </summary>
    public class Report : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            switch (context.Request["report"])
            {
                case "UsersForAll":
                    {
                        var res = "[" + Provider.Database.GetDataTable(@"select
	                            TIMESTAMPDIFF(SECOND, '1970-01-01', concat(Tarih,'-01'))*1000 as t,
	                            Adet as f
                            from (
                            select 
	                            DATE_FORMAT([InsertDate], '%Y.%m') as Tarih, 
	                            count(*) as Adet 
                            from 
	                            User
                            group by 1
                            order by 1) as t;").Rows.Cast<DataRow>().Select(dr => "[" + dr["t"] + "," + dr["f"] + "]").ToList().StringJoin(",") + "]";

                        context.Response.Write("{isError:false, data:" + res + "}");
                        break;
                    }
                case "UsersForLastWeek":
                    {
                        var res = "[" + Provider.Database.GetDataTable(@"select
	                            TIMESTAMPDIFF(SECOND, '1970-01-01', Tarih)*1000 as t,
	                            Adet as f
                            from (
                            select 
	                            DATE_FORMAT([InsertDate], '%Y.%m.%d') as Tarih, 
	                            count(*) as Adet 
                            from 
	                            User
                            where
                                InsertDate >= NOW() - INTERVAL 1 WEEK
                            group by 1
                            order by 1) as t;").Rows.Cast<DataRow>().Select(dr => "[" + dr["t"] + "," + dr["f"] + "]").ToList().StringJoin(",") + "]";

                        context.Response.Write("{isError:false, data:" + res + "}");
                        break;
                    }
                case "UsersForLastMonth":
                    {
                        var res = "[" + Provider.Database.GetDataTable(@"select
	                            TIMESTAMPDIFF(SECOND, '1970-01-01', Tarih)*1000 as t,
	                            Adet as f
                            from (
                            select 
	                            DATE_FORMAT([InsertDate], '%Y.%m.%d') as Tarih, 
	                            count(*) as Adet 
                            from 
	                            User
                            where
                                InsertDate >= NOW() - INTERVAL 1 MONTH
                            group by 1
                            order by 1) as t;").Rows.Cast<DataRow>().Select(dr => "[" + dr["t"] + "," + dr["f"] + "]").ToList().StringJoin(",") + "]";

                        context.Response.Write("{isError:false, data:" + res + "}");
                        break;
                    }
                case "UsersForLastYear":
                    {
                        var res = "[" + Provider.Database.GetDataTable(@"select
	                            TIMESTAMPDIFF(SECOND, '1970-01-01', Tarih)*1000 as t,
	                            Adet as f
                            from (
                            select 
	                            DATE_FORMAT([InsertDate], '%Y.%m.%d') as Tarih, 
	                            count(*) as Adet 
                            from 
	                            User
                            where
                                InsertDate >= NOW() - INTERVAL 1 YEAR
                            group by 1
                            order by 1) as t;").Rows.Cast<DataRow>().Select(dr => "[" + dr["t"] + "," + dr["f"] + "]").ToList().StringJoin(",") + "]";

                        context.Response.Write("{isError:false, data:" + res + "}");
                        break;
                    }
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