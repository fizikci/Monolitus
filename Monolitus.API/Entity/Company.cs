using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Company : NamedEntity
    {
        public string Description { get; set; }

        public string LogoPath {get; set;}
        public string SectorId {get; set;}
        public string Url {get; set;}

        public string AuthUserId {get; set;}
        
        public int SmsCount {get; set;}
        public int GsmCount {get; set;}
        public int EmailCount {get; set;}
        
        public string ApiSmsAllowLink {get; set;}
        public string ApiSmsRejectLink {get; set;}
        public string ApiEmailAllowLink {get; set;}
        public string ApiEmailRejectLink {get; set;}
        public string ApiGsmAllowLink {get; set;}
        public string ApiGsmRejectLink {get; set;}
        public string ApiEmailChangeLink {get; set;}
        public string ApiPhoneChangeLink {get; set;}

        public string CoverPicture { get; set; }
        public bool KisaNo { get; set; }
        public string KisaNoKeyword { get; set; }
        public string KisaNoUserName { get; set; }
        public string KisaNoPassword { get; set; }

        public bool KampanyaVar { get; set; }
        public bool OneCikar { get; set; }


        public override void BeforeSave()
        {
            base.BeforeSave();

            if (
                (!ApiSmsAllowLink.IsEmpty() && !ApiSmsAllowLink.Contains("[USER_ID]")) ||
                (!ApiSmsRejectLink.IsEmpty() && !ApiSmsRejectLink.Contains("[USER_ID]")) ||
                (!ApiEmailAllowLink.IsEmpty() && !ApiEmailAllowLink.Contains("[USER_ID]")) ||
                (!ApiEmailRejectLink.IsEmpty() && !ApiEmailRejectLink.Contains("[USER_ID]")) ||
                (!ApiGsmAllowLink.IsEmpty() && !ApiGsmAllowLink.Contains("[USER_ID]")) ||
                (!ApiGsmRejectLink.IsEmpty() && !ApiGsmRejectLink.Contains("[USER_ID]"))
                )
                throw new Exception("Link [USER_ID] içermeli");

            if(!ApiEmailChangeLink.IsEmpty() && !ApiEmailChangeLink.Contains("[NEW_EMAIL]"))
                throw new Exception("Link [NEW_EMAIL] içermeli");

            if(!ApiPhoneChangeLink.IsEmpty() && !ApiPhoneChangeLink.Contains("[NEW_PHONE]"))
                throw new Exception("Link [NEW_PHONE] içermeli");
        }
    }

}
