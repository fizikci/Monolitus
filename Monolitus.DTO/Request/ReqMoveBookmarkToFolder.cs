namespace Monolitus.DTO.Request
{
    public class ReqMoveBookmarkToFolder : BaseRequest
    {
        public string BookmarkId { get; set; }
        public string FolderId { get; set; }
    }
}
