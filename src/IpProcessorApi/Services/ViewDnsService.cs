using System.Collections.Generic;
using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Http.Services;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public class ViewDnsService : IViewDnsService
    {
        private readonly IHttpViewDnsService _httpViewDnsService;
        private readonly ILogHelper _logHelper;

        public ViewDnsService(IHttpViewDnsService httpViewDnsService, ILogHelper logHelper)
        {
            _httpViewDnsService = httpViewDnsService;
            _logHelper = logHelper;
        }

        public async Task<EntityMetadata<ViewDnsGeoIpResponseLocation, ViewDnsErrors>> GetGeoIp(string ip)
        {
            var response = await _httpViewDnsService.GetGeoIp(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsGeoIpResponseLocation>(null, ViewDnsErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        public async Task<EntityMetadata<ViewDnsPingResponseReplies, ViewDnsErrors>> GetPing(string ip)
        {
            var response = await _httpViewDnsService.GetPing(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsPingResponseReplies>(null, ViewDnsErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        public async Task<EntityMetadata<ViewDnsRdns, ViewDnsErrors>> GetRdns(string ip)
        {

            var response = await _httpViewDnsService.GetRdns(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsRdns>(null, ViewDnsErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        private EntityMetadata<TEntity, ViewDnsErrors> GetEntityMetadataResponse<TEntity>(TEntity obj, ViewDnsErrors? error = null)
        {
            if (obj != null)
            {
                return new EntityMetadata<TEntity, ViewDnsErrors>(obj);
            }

            if (error == null)
            {
                return new EntityMetadata<TEntity, ViewDnsErrors>(
                    new List<ErrorInfo<ViewDnsErrors>>
                    {
                        new ErrorInfo<ViewDnsErrors>(ViewDnsErrors.UnhandledError)
                    });
            }

            return new EntityMetadata<TEntity, ViewDnsErrors>(
                new List<ErrorInfo<ViewDnsErrors>>
                {
                    new ErrorInfo<ViewDnsErrors>(error.GetValueOrDefault())
                });
        }
    }
}
  