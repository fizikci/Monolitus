using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.DTO.Request
{
    public class ReqChangeMemberPassword : BaseRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
