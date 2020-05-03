using System.Collections.Generic;
using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Http.Services;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public class RdapService : IRdapService
    {
        private readonly IHttpRdapService _httpRdapService;

        public RdapService(IHttpRdapService httpRdapService)
        {
            _httpRdapService = httpRdapService;
        }

        public async Task<EntityMetadata<RdapResponse, IpProcessorErrors>> GetRdap(string ip)
        {
            var response = await _httpRdapService.GetRdap(ip);
            if (response == null)
            {
                return new EntityMetadata<RdapResponse, IpProcessorErrors>(
                    new List<ErrorInfo<IpProcessorErrors>>
                    {
                        new ErrorInfo<IpProcessorErrors>(IpProcessorErrors.InvalidResponse)
                    });
            }

            return new EntityMetadata<RdapResponse, IpProcessorErrors>(response);
        }
    }
}
