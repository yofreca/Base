using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastruture.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException) context.Exception;
                var validation = new
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Description = "Bad Request",
                    Detail = exception.Message
                };

                var json = new
                {
                    errors = new[] {validation}
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
