using System.Threading.Tasks;
using IpProcessorApi.Http.Responses;

namespace IpProcessorApi.Http.Services
{
    public interface IHttpRdapService
    {
        Task<RdapResponse> GetRdap(string ip);
    }
}
