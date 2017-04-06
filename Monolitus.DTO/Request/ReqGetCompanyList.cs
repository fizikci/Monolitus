namespace Monolitus.DTO.Request
{
    public class ReqGetCompanyList : BaseRequest
    {
        public string Query { get; set; }
        public string SectorId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
