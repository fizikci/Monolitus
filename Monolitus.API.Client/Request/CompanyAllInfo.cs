namespace Monolitus.API.Client.Request
{
    public class CompanyAllInfo : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string LogoPath { get; set; }
        public string SectorId { get; set; }
        public string Url { get; set; }

        public int SmsCount { get; set; }
        public int GsmCount { get; set; }
        public int EmailCount { get; set; }

        public string ApiSmsAllowLink { get; set; }
        public string ApiSmsRejectLink { get; set; }
        public string ApiEmailAllowLink { get; set; }
        public string ApiEmailRejectLink { get; set; }
        public string ApiGsmAllowLink { get; set; }
        public string ApiGsmRejectLink { get; set; }
        public string ApiEmailChangeLink { get; set; }
        public string ApiPhoneChangeLink { get; set; }
    }
}
