namespace Monolitus.DTO.Request
{
    public class ReqGetList : BaseRequest
    {
        public string Query { get; set; }
        public string SortBy { get; set; }
        public string SortValue { get; set; }
        public string UserId { get; set; }
    }
}
