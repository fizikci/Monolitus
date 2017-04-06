using Monolitus.DTO.Enums;
namespace Monolitus.DTO.Request
{
    public class ReqSignUp : BaseRequest
    {
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string DogumTarihi { get; set; }
        public Cinsiyet Gender { get; set; }

        public string City { get; set; }
    }
}
