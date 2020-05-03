using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;

namespace IpProcessorApi.Http.Services
{
    public interface IHttpViewDnsService
    {
        Task<ViewDnsGeoIpResponse> GetGeoIp(string ip);

        Task<ViewDnsPingResponse> GetPing(string ip);

        Task<ViewDnsRdnsResponse> GetRdns(string ip);
    }
}
