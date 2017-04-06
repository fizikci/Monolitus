using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class SmsSend : BaseEntity
    {
        public string CompanyId {get; set;}
        public string MessageText {get; set;}
        public int ToUsers {get; set;}
        public string UsersFilter {get; set;}


    }

}
