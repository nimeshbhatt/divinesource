using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Services;
using Domain.Global;
using Domain.Handler;
using Domain.Enum;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class ServiceQuotationController : BaseApiController
    {
        private readonly IImageHandler _imageHandler;
        private readonly IServiceQuotationService _serviceQuotationService;

        public ServiceQuotationController(IImageHandler imageHandler, IServiceQuotationService serviceQuotationService)
        {
            _imageHandler = imageHandler;
            _serviceQuotationService = serviceQuotationService;
        }

        [HttpPost("replytoservice")]
        public async Task<ObjectResult> ReplyToService(Guid serviceId, Guid serviceEngId, DateTime dateTime)
        {
            ObjectResult objectResult;
            try
            {
                var updateEng = await _serviceQuotationService.ReplyToService(serviceId, serviceEngId, dateTime);
                objectResult = GetObjectResponse(string.Empty, updateEng);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpPost("addservicerequest")]
        public async Task<IActionResult> AddServiceRequest(IFormFileCollection file, ServiceTypeEnum x, Guid UserId, string title, string description)
        {
            List<IActionResult> list = new List<IActionResult>();

            // Validation
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                return BadRequest("Title or description field is required");
            }

            list.Add(_imageHandler.AddServiceRequest(file, x, UserId, title, description).Result);
            return Ok(list);
        }

        [HttpGet("servicequotationlistbyid/{id}")]
        public async Task<IActionResult> GetServiceQutationRequest(Guid id)
        {
            return Ok(await _serviceQuotationService.GetServiceQuotationById(id));

        }

        [HttpGet("servicequotationlist/{x}")]
        public async Task<IActionResult> ServiceQuotationList(ServiceTypeEnum x)
        {
            ObjectResult objectResult;

            try
            {
                var serviceQuotationList = await _serviceQuotationService.ServiceQuotationList(x);
                objectResult = serviceQuotationList != null && serviceQuotationList.Count > 0
                    ? GetObjectResponse(string.Empty, serviceQuotationList) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;

        }
    }
}
