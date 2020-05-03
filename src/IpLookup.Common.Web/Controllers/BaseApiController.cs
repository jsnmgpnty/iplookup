using IpLookup.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IpLookup.Common.Web.Controllers
{
    [Produces("application/json")]
    public class BaseApiController : Controller
    {
        protected readonly ILogHelper _logHelper;
        protected readonly IConfigurationHelper _configurationHelper;

        public BaseApiController(ILogHelper logHelper, IConfigurationHelper configurationHelper)
        {
            _logHelper = logHelper;
            _configurationHelper = configurationHelper;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetErrorResponse(Exception exception, string message)
        {
            LogError(message, exception);
            return BadRequest(message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetErrorResponse(string message)
        {
            LogError(message);
            return BadRequest(message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetSuccessResponse<T>(bool success, string message, T data)
        {
            return Ok(new BaseResponse<T>
            {
                IsSuccess = success,
                Message = message,
                Data = data
            });
        }

        public void LogError(string message, Exception exception = null)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = ControllerContext.RouteData.Values["action"].ToString();
            _logHelper.LogError(exception, $"{controllerName}.{actionName} - Error calling {actionName} - {message}");
        }
    }
}