namespace Monolitus.DTO.Request
{
    public class ReqAddBookmark : BaseRequest
    {
        public string Url { get; set; }
        public string FolderId { get; set; }
    }
}
