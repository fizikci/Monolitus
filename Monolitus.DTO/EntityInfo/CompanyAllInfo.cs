using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.EntityInfo
{
    public class CompanyAllInfo : NamedEntityInfo
    {
        public string LogoPath { get; set; }
        public string SectorId { get; set; }
        public string Url { get; set; }

        public int SmsCount { get; set; }
        public int GsmCount { get; set; }
        public int EmailCount { get; set; }

        public string ApiSmsAllowLink { get; set; }
        public string ApiSmsRejectLink { get; set; }
        public string ApiEmailAllowLink { get; set; }
        public string ApiEmailRejectLink { get; set; }
        public string ApiGsmAllowLink { get; set; }
        public string ApiGsmRejectLink { get; set; }
        public string ApiEmailChangeLink { get; set; }
        public string ApiPhoneChangeLink { get; set; }

        public string CoverPicture { get; set; }
        public bool KisaNo { get; set; }
        public string KisaNoKeyword { get; set; }
        public string KisaNoUserName { get; set; }
        public string KisaNoPassword { get; set; }
        public bool KampanyaVar { get; set; }
        public bool OneCikar { get; set; }
    }
}
