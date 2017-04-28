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

        public bool EmailValidated { get; set; }

        public string Surname { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
