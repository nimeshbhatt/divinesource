using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Domain;
using Domain.Global;

namespace Core.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public ObjectResult GetObjectResponse(string message, dynamic data = null, HttpStatusCode statusCode = HttpStatusCode.OK, bool isSuccess = true)
        {
            var statusResponse = new StatusResponse
            {
                StatusCode = statusCode,
                IsSuccess = isSuccess,
                Data = data,
                Message = message
            };

            return new ObjectResult(statusResponse);
        }

        //public string GetCurrentUserRole()
        //{
        //    return Request.Headers["Role"];
        //}
    }
}
