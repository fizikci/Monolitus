using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Message : BaseEntity
    {
        public string UserId {get; set;}
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }

    public class ListViewMessage : Message
    {
        public string UserName { get; set; }
    }
}
