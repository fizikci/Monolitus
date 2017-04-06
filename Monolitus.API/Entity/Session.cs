using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Session : BaseEntity
    {
        public string UserId {get; set;}
        public DateTime LoginDate {get; set;}
        public DateTime LastAccess {get; set;}


        public string SerializedParams { get; set; }

        public User GetUser() { return Provider.ReadEntityWithRequestCache<User>(UserId); }

        private SessionParams _params;
        public SessionParams Params
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SerializedParams))
                {
                    _params = new SessionParams();
                    return _params;
                }

                if (_params == null)
                    _params = SerializedParams.Deserialize<SessionParams>();

                return _params;
            }
        }

    }

    public class SessionParams
    {
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
    }
}
