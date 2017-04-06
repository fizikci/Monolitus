using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.API.Client.Request
{
    public class UserInfo : BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
