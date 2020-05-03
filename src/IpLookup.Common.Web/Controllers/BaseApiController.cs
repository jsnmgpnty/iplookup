using IpLookup.Common.Extensions;
using IpLookup.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = ControllerContext.RouteData.Values["action"].ToString();
            _logHelper.LogError(exception, $"{controllerName}.{actionName} - Error calling {actionName} - {message}");

            return BadRequest(message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetErrorResponse(string message)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = ControllerContext.RouteData.Values["action"].ToString();
            _logHelper.LogError($"{controllerName}.{actionName} - Error calling {actionName} - {message}");

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleResponse<T, TE>(EntityMetadata<T, TE> result, string defaultError) where TE : struct
        {
            if (result == null)
            {
                return GetErrorResponse(defaultError);
            }

            if (result.Errors?.Count > 0 || result.Data == null)
            {
                var error = result.Errors.First();
                return GetErrorResponse(error.ErrorType.ToEnumString());
            }

            return GetSuccessResponse(true, null, result.Data);
        }
    }
}