using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IpLookup.Models;
using IpLookupApi.Http.Responses;
using IpLookupApi.Http.Services;
using IpLookupApi.Models;

namespace IpLookupApi.Services
{
    public class IpStatusService : IIpStatusService
    {
        private readonly IHttpIpProcessorService _httpIpProcessorService;

        public IpStatusService(IHttpIpProcessorService httpIpProcessorService)
        {
            _httpIpProcessorService = httpIpProcessorService;
        }

        public async Task<EntityMetadata<IpStatus, IpLookupErrors>> GetIpStatus(string ip, List<string> services)
        {
            var tasks = new List<IpTask<IpProcessorResponse>>();

            foreach (var svc in services)
            {
                switch (svc.ToLower())
                {
                    case "rdap":
                        tasks.Add(new IpTask<IpProcessorResponse>("rdap", _httpIpProcessorService.GetRdap(ip)));
                        break;
                    case "rdns":
                        tasks.Add(new IpTask<IpProcessorResponse>("rdns", _httpIpProcessorService.GetRdns(ip)));
                        break;
                    case "ping":
                        tasks.Add(new IpTask<IpProcessorResponse>("ping", _httpIpProcessorService.GetPing(ip)));
                        break;
                    case "geo":
                        tasks.Add(new IpTask<IpProcessorResponse>("geo", _httpIpProcessorService.GetGeo(ip)));
                        break;
                    default:
                        break;
                }
            }

            await Task.WhenAll(tasks.Select(s => s.Task));

            var errors = new List<ErrorInfo<IpLookupErrors>>();
            var result = new IpStatus();

            foreach (var t in tasks)
            {
                var taskResult = await t.Task;

                if (taskResult == null)
                {
                    errors.Add(new ErrorInfo<IpLookupErrors>(GetErrorType(t.ServiceName)));
                    continue;
                }

                switch (t.ServiceName.ToLower())
                {
                    case "rdap":
                        result.Rdap = taskResult as RdapResponse;
                        break;
                    case "rdns":
                        result.Rdns = taskResult as RdnsResponse;
                        break;
                    case "ping":
                        result.Ping = taskResult as PingResponse;
                        break;
                    case "geo":
                        result.GeoIp = taskResult as GeoIpResponse;
                        break;
                    default:
                        break;
                }
            }

            var entityResponse = new EntityMetadata<IpStatus, IpLookupErrors>(result, errors);
            return entityResponse;
        }

        private IpLookupErrors GetErrorType(string name)
        {
            switch (name.ToLower())
            {
                case "rdap":
                    return IpLookupErrors.RdapResponseError;
                case "rdns":
                    return IpLookupErrors.RdnsResponseError;
                case "ping":
                    return IpLookupErrors.PingResponseError;
                case "geo":
                    return IpLookupErrors.GeoIpResponseError;
                default:
                    return IpLookupErrors.UnhandledError;
            }
        }
    }
}
