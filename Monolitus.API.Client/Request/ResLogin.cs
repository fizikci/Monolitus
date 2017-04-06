namespace Monolitus.API.Client.Request
{
    public class ResLogin
    {
        public string SessionId { get; set; }
        public UserInfo User { get; set; }
    }
}
