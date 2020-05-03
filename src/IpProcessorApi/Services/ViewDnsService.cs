using System.Collections.Generic;
using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Http.Services;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public class ViewDnsService : IViewDnsService
    {
        private readonly IHttpViewDnsService _httpViewDnsService;

        public ViewDnsService(IHttpViewDnsService httpViewDnsService)
        {
            _httpViewDnsService = httpViewDnsService;
        }

        public async Task<EntityMetadata<ViewDnsGeoIpResponseLocation, IpProcessorErrors>> GetGeoIp(string ip)
        {
            var response = await _httpViewDnsService.GetGeoIp(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsGeoIpResponseLocation>(null, IpProcessorErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        public async Task<EntityMetadata<ViewDnsPingResponseReplies, IpProcessorErrors>> GetPing(string ip)
        {
            var response = await _httpViewDnsService.GetPing(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsPingResponseReplies>(null, IpProcessorErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        public async Task<EntityMetadata<ViewDnsRdns, IpProcessorErrors>> GetRdns(string ip)
        {
            var response = await _httpViewDnsService.GetRdns(ip);
            if (response?.Response == null)
            {
                return GetEntityMetadataResponse<ViewDnsRdns>(null, IpProcessorErrors.InvalidResponse);
            }

            return GetEntityMetadataResponse(response.Response);
        }

        private EntityMetadata<TEntity, IpProcessorErrors> GetEntityMetadataResponse<TEntity>(TEntity obj, IpProcessorErrors? error = null)
        {
            if (obj != null)
            {
                return new EntityMetadata<TEntity, IpProcessorErrors>(obj);
            }

            if (error == null)
            {
                return new EntityMetadata<TEntity, IpProcessorErrors>(
                    new List<ErrorInfo<IpProcessorErrors>>
                    {
                        new ErrorInfo<IpProcessorErrors>(IpProcessorErrors.UnhandledError)
                    });
            }

            return new EntityMetadata<TEntity, IpProcessorErrors>(
                new List<ErrorInfo<IpProcessorErrors>>
                {
                    new ErrorInfo<IpProcessorErrors>(error.GetValueOrDefault())
                });
        }
    }
}
  