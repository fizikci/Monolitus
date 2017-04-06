using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class CompanyApplication : NamedEntity
    {
        public string AuthName {get; set;}
        public string Email {get; set;}
        public string PhoneCell {get; set;}
        public string City {get; set;}
        public string Url {get; set;}
        public bool Applied {get; set;}

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (!isUpdate) 
                Provider.SendMail("Yeni kurumsal başvuru (" + Name + ")", this.ToHtmlTable());
        }
    }

}
