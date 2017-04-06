using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.DTO.EntityInfo
{
    public class MessageInfo : BaseEntityInfo
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }

}
