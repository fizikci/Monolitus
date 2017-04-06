using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolitus.DTO.Enums;

namespace Monolitus.DTO.Request
{
    public class ProfileInfo : BaseRequest
    {
        public string City { get; set; }
        public Cinsiyet Gender { get; set; }
        public string PhoneCell { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DogumTarihi { get; set; }

        public string EgitimDurumu { get; set; }
        public string MedeniHal { get; set; }
        public string Ehliyet { get; set; }
        public string Otomobil { get; set; }
        public string Ilce { get; set; }
        public string Meslek { get; set; }
        public string YabanciDil { get; set; }
        public string KanGrubu { get; set; }
        public string IlgiAlanlari { get; set; }
        public string Takim { get; set; }
        public int AylikGelir { get; set; }
        public string EviVar { get; set; }
        public string TelOperator { get; set; }
        public string SaglikSigorta { get; set; }
        public string IsDurumu { get; set; }
        public string SirketDurumu { get; set; }
    }
}
