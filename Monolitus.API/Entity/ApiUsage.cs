using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class ApiUsage : BaseEntity
    {
        public string UserId {get; set;}
        public string MethodName {get; set;}
        public bool Successful {get; set;}
        public int ProcessTime {get; set;}


    }

}
