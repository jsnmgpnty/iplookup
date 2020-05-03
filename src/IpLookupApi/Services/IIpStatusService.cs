using System.Collections.Generic;
using System.Threading.Tasks;
using IpLookup.Models;
using IpLookupApi.Models;

namespace IpLookupApi.Services
{
    public interface IIpStatusService
    {
        Task<EntityMetadata<IpStatus, IpLookupErrors>> GetIpStatus(string ip, List<string> services);
    }
}
