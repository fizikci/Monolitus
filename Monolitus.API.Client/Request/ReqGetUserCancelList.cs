using System;

namespace Monolitus.API.Client.Request
{
    public class ReqGetUserCancelList : BaseRequest
    {
        public string Type { get; set; }
        public DateTime Since { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
