using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.DTO.EntityInfo
{
    public class CompanyInfo : NamedEntityInfo
    {
        public string Description { get; set; }

        public string LogoPath {get; set;}
        public string SectorId {get; set;}
        public string Url {get; set;}
        
        public int SmsCount {get; set;}
        public int GsmCount {get; set;}
        public int EmailCount {get; set;}

        public string Sms { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }

        public string CoverPicture { get; set; }
        public bool KisaNo { get; set; }
        public string KisaNoKeyword { get; set; }
        public string KisaNoUserName { get; set; }
        public string KisaNoPassword { get; set; }
        public bool KampanyaVar { get; set; }
        public bool OneCikar { get; set; }
    }

}
