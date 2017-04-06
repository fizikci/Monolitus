using Cinar.Database;
using Monolitus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class CompanyKampanya : BaseEntity
    {
        public string CompanyId { get; set; }
        public string Title {get; set;}
        public string Description {get; set;}

        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }

        Company c;

        public override void BeforeSave()
        {
            base.BeforeSave();

            c = Provider.Database.Read<Company>("Id={0}", CompanyId);
            if (c == null)
                throw new APIException("Firma belirtilmeli");
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            c.KampanyaVar = Provider.Database.GetInt("select count(*) from CompanyKampanya where Baslangic<=now() AND Bitis>=now()") > 0;
            c.Save();
        }

        public override void Delete()
        {
            Provider.Database.ExecuteNonQuery("DELETE FROM CompanyKampanya WHERE Id={0}", Id);
            Company c2 = Provider.Database.Read<Company>("Id={0}", CompanyId);
            c2.KampanyaVar = Provider.Database.GetInt("select count(*) from CompanyKampanya where Baslangic<=now() AND Bitis>=now()") > 0;
            c2.Save();

        }
    }

}
