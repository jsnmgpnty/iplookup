using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public interface IViewDnsService
    {
        Task<EntityMetadata<ViewDnsGeoIpResponseLocation, IpProcessorErrors>> GetGeoIp(string ip);

        Task<EntityMetadata<ViewDnsPingResponseReplies, IpProcessorErrors>> GetPing(string ip);

        Task<EntityMetadata<ViewDnsRdns, IpProcessorErrors>> GetRdns(string ip);
    }
}
