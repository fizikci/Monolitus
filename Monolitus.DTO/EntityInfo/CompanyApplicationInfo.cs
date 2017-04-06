using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.DTO.EntityInfo
{
    public class CompanyApplicationInfo : NamedEntityInfo
    {
        public string AuthName { get; set; }
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public string City { get; set; }
        public string Url { get; set; }
        public bool Applied { get; set; }
    }

}
