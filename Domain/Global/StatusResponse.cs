using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Global
{
    public class StatusResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
