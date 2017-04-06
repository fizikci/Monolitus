using System;

namespace Monolitus.DTO.Request
{
    public class ReqGetUserCancelList : BaseRequest
    {
        public string Type { get; set; }
        public DateTime Since { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
