using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolitus.DTO.Enums;

namespace Monolitus.DTO.EntityInfo
{
    public class ListViewUserCompanyInfo : BaseEntityInfo
    {
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public bool Sms { get; set; }
        public bool Gsm { get; set; }
        public bool Email { get; set; }
        public DateTime SmsUpdateDate { get; set; }
        public DateTime GsmUpdateDate { get; set; }
        public DateTime EmailUpdateDate { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneCell { get; set; }
    }
}
