namespace Monolitus.API.Client.Request
{
    public class ReqGetUserList : BaseRequest
    {
        public string Query { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public string Gsm { get; set; } // "1", "0", null
        public string Sms { get; set; }
        public string Email { get; set; }
    }
}
