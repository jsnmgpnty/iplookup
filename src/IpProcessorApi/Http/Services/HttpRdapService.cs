using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Services.Http;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Http.Settings;
using Newtonsoft.Json;
using Serilog;

namespace IpProcessorApi.Http.Services
{
    public class HttpRdapService : IHttpRdapService
    {
        private readonly ILogHelper _logHelper;
        private readonly HttpClientWrapper _httpClient;
        private readonly HttpRdapServiceSettings _settings;

        public HttpRdapService(ILogHelper logHelper, ILogger logger, HttpRdapServiceSettings httpSettings, JsonSerializerSettings serializerSettings)
        {
            _logHelper = logHelper;
            _settings = httpSettings;
            _httpClient = new HttpClientWrapper(logger, httpSettings.BaseUrl, httpSettings.TimeoutInMilliseconds, serializerSettings);
        }

        public async Task<RdapResponse> GetRdap(string ip)
        {
            _logHelper.LogInfo($"HttpRdapService - GetRdap - ip: {ip}");
            return await _httpClient.GetAsync<RdapResponse>($"ip/{ip}");
        }
    }
}
