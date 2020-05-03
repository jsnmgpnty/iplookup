using System;
using System.Linq;
using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Common.Extensions;
using IpLookup.Common.Web.Controllers;
using IpLookup.Models;
using IpProcessorApi.Models;
using IpProcessorApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IpProcessorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : BaseApiController
    {
        private readonly IViewDnsService _viewDnsService;
        private readonly IRdapService _rdapService;

        public IpController(IViewDnsService viewDnsService, IRdapService rdapService, ILogHelper logHelper, IConfigurationHelper configurationHelper)
            : base(logHelper, configurationHelper)
        {
            _viewDnsService = viewDnsService;
            _rdapService = rdapService;
        }

        [HttpGet]
        [Route("{ip}/geo")]
        public async Task<IActionResult> GetGeoIp(string ip)
        {
            try
            {
                var result = await _viewDnsService.GetGeoIp(ip);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex, "Unhandled error occurred");
            }
        }

        [HttpGet]
        [Route("{ip}/ping")]
        public async Task<IActionResult> GetPing(string ip)
        {
            try
            {
                var result = await _viewDnsService.GetPing(ip);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex, "Unhandled error occurred");
            }
        }

        [HttpGet]
        [Route("{ip}/rdns")]
        public async Task<IActionResult> GetRdns(string ip)
        {
            try
            {
                var result = await _viewDnsService.GetRdns(ip);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex, "Unhandled error occurred");
            }
        }

        [HttpGet]
        [Route("{ip}/rdap")]
        public async Task<IActionResult> GetRdap(string ip)
        {
            try
            {
                var result = await _rdapService.GetRdap(ip);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex, "Unhandled error occurred");
            }
        }

        private IActionResult HandleResponse<T>(EntityMetadata<T, IpProcessorErrors> result)
        {
            if (result == null)
            {
                return GetErrorResponse(IpProcessorErrors.UnhandledError.ToEnumString());
            }

            if (result.Errors?.Count > 0)
            {
                var error = result.Errors.FirstOrDefault();
                switch (error.ErrorType)
                {
                    case IpProcessorErrors.NotFound:
                        LogError("Result not found");
                        return NotFound();
                    case IpProcessorErrors.Unauthorized:
                        LogError("Request is unauthorized");
                        return Unauthorized();
                    default:
                        return GetErrorResponse(error.ErrorType.ToEnumString());
                }
            }

            return GetSuccessResponse(true, null, result.Data);
        }
    }
}
