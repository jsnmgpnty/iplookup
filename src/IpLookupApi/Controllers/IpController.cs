using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Common.Extensions;
using IpLookup.Common.Web.Controllers;
using IpLookupApi.Models;
using IpLookupApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IpLookupApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : BaseApiController
    {
        private readonly IIpStatusService _ipStatusService;

        public IpController(IIpStatusService ipStatusService, ILogHelper logHelper, IConfigurationHelper configurationHelper)
            : base(logHelper, configurationHelper)
        {
            _ipStatusService = ipStatusService;
        }

        [HttpGet]
        [Route("status/{ip}")]
        public async Task<IActionResult> GetStatus(string ip, [FromQuery]string services)
        {
            var servicesList = GetServices(services);

            try
            {
                var result = await _ipStatusService.GetIpStatus(ip, servicesList);
                if (result == null)
                {
                    return GetErrorResponse(IpLookupErrors.UnhandledError.ToEnumString());
                }

                if (result.Errors?.Count > 0)
                {
                    var error = result.Errors.FirstOrDefault();
                    return GetErrorResponse(error.ErrorType.ToEnumString());
                }

                return GetSuccessResponse(true, null, result.Data);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex, "Unhandled error occurred");
            }
        }

        // TODO: Ideally should be a model binder
        private List<string> GetServices(string services)
        {
            var arr = services.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return arr.ToList();
        }
    }
}
