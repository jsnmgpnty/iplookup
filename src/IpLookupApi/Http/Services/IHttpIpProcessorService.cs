using System.Threading.Tasks;
using IpLookup.Models;
using IpLookupApi.Http.Responses;

namespace IpLookupApi.Http.Services
{
    public interface IHttpIpProcessorService
    {
        Task<IpProcessorResponse> GetRdap(string ip);
        Task<IpProcessorResponse> GetPing(string ip);
        Task<IpProcessorResponse> GetGeo(string ip);
        Task<IpProcessorResponse> GetRdns(string ip);
    }
}
