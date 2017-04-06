namespace Monolitus.DTO.Request
{
    public class ReqChangeForgottenPassword : BaseRequest
    {
        public string Keyword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
