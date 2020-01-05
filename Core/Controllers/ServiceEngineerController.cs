using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Service;
using Service.Interface;
using Service.Services;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Domain.Context;
using Domain.Global;
using System.Linq;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class ServiceEngineerController : BaseApiController
    {
        private readonly IServiceEngineer _serviceEngineer;

        public ServiceEngineerController(IServiceEngineer serviceEngineer)
        {
            _serviceEngineer = serviceEngineer;
        }

        [HttpPost("create")]
        public async Task<ObjectResult> Create([FromBody]ServiceEngineerAc serviceEngineer)
        {
            ObjectResult objectResult;
            try
            {
                var engineerAc = await _serviceEngineer.Create(serviceEngineer);
                objectResult = engineerAc != null ? GetObjectResponse(string.Empty, engineerAc)
                    : GetObjectResponse(
                        Constants.UserNameAlreadyExists, statusCode: System.Net.HttpStatusCode.BadRequest, isSuccess: false);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
            return objectResult;
        }

        [HttpGet("engineerlist")]
        public async Task<ObjectResult> ListEngineer()
        {
            ObjectResult objectResult;
            try
            {

                var engList = await _serviceEngineer.ListServiceEngineer();
                objectResult = engList != null && engList.Count > 0
                    ? GetObjectResponse(string.Empty, engList) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpPut("engineerupdate")]
        public async Task<ObjectResult> Update([FromBody]ServiceEngineerAc serviceEngineerAc)
        {
            ObjectResult objectResult;
            try
            {
                var updateEng = await _serviceEngineer.Update(serviceEngineerAc);
                objectResult = GetObjectResponse(string.Empty, updateEng);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpDelete("engineerdelete/{id}")]
        public async Task<ObjectResult> Delete(string id)
        {
            ObjectResult objectResult;
            
            try
            {
                var status = await _serviceEngineer.Delete(id);
                objectResult = status != null
                    ? GetObjectResponse(string.Empty, status) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch(Exception ex)
            {
                return GetObjectResponse("Service Engineer Not Found", isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpGet("engineerbyid/{id}")]
        public async Task<ObjectResult> GetById(string id)
        {
            ObjectResult objectResult;

            try
            {
                var status = await _serviceEngineer.GetById(id);
                objectResult = status != null
                    ? GetObjectResponse(string.Empty, status) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch(Exception ex)
            {
                return GetObjectResponse("Service Engineer Not Found", isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }
    }
}
