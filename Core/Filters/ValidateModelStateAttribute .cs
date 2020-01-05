using Domain.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors);
                var errorList = new List<string>();
                errors.ToList().ForEach(x =>
                 {
                     errorList.Add(x.ErrorMessage);
                 });

                var statusResponse = new StatusResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Data = null,
                    Message = string.Join(",", errorList)
                };

                var obj = new ObjectResult(statusResponse);
                context.Result = obj;
            }
        }
    }
}