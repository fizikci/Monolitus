namespace Monolitus.DTO.Request
{
    public class ReqAddFolder : BaseRequest
    {
        public string Name { get; set; }
        public string FolderId { get; set; }
    }
}
