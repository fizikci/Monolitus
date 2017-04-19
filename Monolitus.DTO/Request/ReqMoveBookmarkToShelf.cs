namespace Monolitus.DTO.Request
{
    public class ReqMoveBookmarkToShelf : BaseRequest
    {
        public string BookmarkId { get; set; }
        public string ShelfId { get; set; }
    }
}
