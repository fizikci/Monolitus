using Cinar.Database;
using Monolitus.DTO;
using Monolitus.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolitus.API.Entity
{
    public class Odeme : BaseEntity
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public int Tutar { get; set; }
        public bool Odendi { get; set; }
        public UrunTypes Urun { get; set; }

        public int Miktar { get; set; }
    }

}
