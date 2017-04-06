using System.Threading.Tasks;
using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class UserCompany : BaseEntity
    {
        public string UserId {get; set;}
        public string CompanyId {get; set;}
        public bool Sms {get; set;}
        public bool Gsm {get; set;}
        public bool Email {get; set;}
        public DateTime SmsUpdateDate {get; set;}
        public DateTime GsmUpdateDate {get; set;}
        public DateTime EmailUpdateDate {get; set;}

        public bool DoneWithKisaNo { get; set; }

        public User GetUser() { return Provider.ReadEntityWithRequestCache<User>(UserId); }
        public Company GetCompany() { return Provider.ReadEntityWithRequestCache<Company>(CompanyId); }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (!Sms.Equals(GetOriginalValues()["Sms"]))
            {
                SmsUpdateDate = DateTime.Now;
                var c = GetCompany();
                Task.Factory.StartNew(() => this.firmayaHaberVer(c, "Sms"));
            }
            if (!Gsm.Equals(GetOriginalValues()["Gsm"]))
            {
                GsmUpdateDate = DateTime.Now;
                var c = GetCompany();
                Task.Factory.StartNew(() => this.firmayaHaberVer(c, "Gsm"));
            }
            if (!Email.Equals(GetOriginalValues()["Email"]))
            {
                EmailUpdateDate = DateTime.Now;
                var c = GetCompany();
                Task.Factory.StartNew(() => this.firmayaHaberVer(c, "Email"));
            }
        }

        private void firmayaHaberVer(Company c, string type)
        {
            try
            {
                var link = "";
                if (type == "Email") link = Email ? c.ApiEmailAllowLink : c.ApiEmailRejectLink;
                if (type == "Gsm") link = Gsm ? c.ApiGsmAllowLink : c.ApiGsmRejectLink;
                if (type == "Sms") link = Sms ? c.ApiSmsAllowLink : c.ApiSmsRejectLink;
                if (!link.IsEmpty())
                    link.Replace("[USER_ID]", UserId).DownloadPage();
            }
            catch
            {
            }
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            Provider.Database.ExecuteNonQuery(@"
                    UPDATE 
                        Company 
                    SET
	                    SmsCount   = (SELECT count(*) FROM UserCompany WHERE Sms=1 AND CompanyId = {0}),
	                    GsmCount   = (SELECT count(*) FROM UserCompany WHERE Gsm=1 AND CompanyId = {0}),
	                    EmailCount = (SELECT count(*) FROM UserCompany WHERE Email=1 AND CompanyId = {0})
                    WHERE
                        Id = {0}", CompanyId);
        }
    }

    public class ListViewUserCompany : UserCompany
    {
        public string Query { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneCell { get; set; }
    }

}
