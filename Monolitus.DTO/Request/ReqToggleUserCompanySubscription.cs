namespace Monolitus.DTO.Request
{
    public class ReqToggleUserCompanySubscription : BaseRequest
    {
        public string CompanyId { get; set; }
        public string Type { get; set; }
    }
}
