using Monolitus.DTO.EntityInfo;

namespace Monolitus.DTO.Response
{
    public class ResLogin
    {
        public string SessionId { get; set; }
        public UserInfo User { get; set; }
    }
}
