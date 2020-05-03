using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public interface IViewDnsService
    {
        Task<EntityMetadata<ViewDnsGeoIpResponseLocation, ViewDnsErrors>> GetGeoIp(string ip);

        Task<EntityMetadata<ViewDnsPingResponseReplies, ViewDnsErrors>> GetPing(string ip);

        Task<EntityMetadata<ViewDnsRdns, ViewDnsErrors>> GetRdns(string ip);
    }
}
