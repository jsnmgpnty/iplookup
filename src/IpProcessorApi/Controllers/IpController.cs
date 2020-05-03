using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Common.Extensions;
using IpLookup.Common.Web.Controllers;
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

        public IpController(IViewDnsService viewDnsService, ILogHelper logHelper, IConfigurationHelper configurationHelper)
            : base(logHelper, configurationHelper)
        {
            _viewDnsService = viewDnsService;
        }

        [HttpGet]
        [Route("geo")]
        public async Task<IActionResult> GetGeoIp(string ip)
        {
            var result = await _viewDnsService.GetGeoIp(ip);
            return HandleResponse(result, ViewDnsErrors.UnhandledError.ToEnumString());
        }

        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> GetPing(string ip)
        {
            var result = await _viewDnsService.GetPing(ip);
            return HandleResponse(result, ViewDnsErrors.UnhandledError.ToEnumString());
        }

        [HttpGet]
        [Route("rdns")]
        public async Task<IActionResult> GetRdns(string ip)
        {
            var result = await _viewDnsService.GetRdns(ip);
            return HandleResponse(result, ViewDnsErrors.UnhandledError.ToEnumString());
        }
    }
}
