using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.DTO.EntityInfo
{
    public class CompanyKampanyaInfo : BaseEntityInfo
    {
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
    }

}
