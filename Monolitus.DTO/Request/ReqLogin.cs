﻿namespace Monolitus.DTO.Request
{
    public class ReqLogin : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
