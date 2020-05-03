using System.Threading.Tasks;
using IpLookup.Models;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Models;

namespace IpProcessorApi.Services
{
    public interface IRdapService
    {
        Task<EntityMetadata<RdapResponse, IpProcessorErrors>> GetRdap(string ip);
    }
}
