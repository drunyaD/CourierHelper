using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CourierHelper.Host.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public string Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; 
            var code = HttpStatusCode.InternalServerError;

            if (exception is ValidationException) code = HttpStatusCode.BadRequest;

            Response.StatusCode = (int) code;

            return exception.Message;
        }
    }
}
