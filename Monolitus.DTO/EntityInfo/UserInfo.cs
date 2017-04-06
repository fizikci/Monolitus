using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolitus.DTO.Enums;

namespace Monolitus.DTO.EntityInfo
{
    public class UserInfo : BaseEntityInfo
    {
        public bool IsAnonim()
        {
            return Id == "";
        }

        public string FullName
        {
            get
            {
                return string.Format(
                                    "{0} {1}",
                                    !String.IsNullOrWhiteSpace(Name) ? Name : Email.Split('@')[0],
                                    !String.IsNullOrWhiteSpace(Surname) ? Surname : "");
            }
        }


        public string Name { get; set; }
        public string Email { get; set; }
        public UserTypes UserType { get; set; }

        public string PhoneCell { get; set; }
        public bool EmailValidated { get; set; }
        public bool PhoneCellValidated { get; set; }

        public string Surname { get; set; }
        public string DogumTarihi { get; set; }
        public Cinsiyet Gender { get; set; }
        public string Avatar { get; set; }
        public string City { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }

        public int ProfileInfoPercent { get; set; }

        public DateTime LastLoginDate { get; set; }

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
