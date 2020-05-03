using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Services.Http;
using IpProcessorApi.Http.Responses;
using IpProcessorApi.Http.Settings;
using Newtonsoft.Json;
using Serilog;

namespace IpProcessorApi.Http.Services
{
    public class HttpViewDnsService : IHttpViewDnsService
    {
        private readonly ILogHelper _logHelper;
        private readonly HttpClientWrapper _httpClient;
        private readonly HttpViewDnsServiceSettings _settings;

        public HttpViewDnsService(ILogHelper logHelper, ILogger logger, HttpViewDnsServiceSettings httpSettings, JsonSerializerSettings serializerSettings)
        {
            _logHelper = logHelper;
            _settings = httpSettings;
            _httpClient = new HttpClientWrapper(logger, httpSettings.BaseUrl, httpSettings.TimeoutInMilliseconds, serializerSettings);
        }

        public async Task<ViewDnsGeoIpResponse> GetGeoIp(string ip)
        {
            _logHelper.LogInfo($"HttpViewDnsService - GetGeoIp - ip: {ip}");
            return await _httpClient.GetAsync<ViewDnsGeoIpResponse>($"iplocation/?output=json&apikey={_settings.ApiKey}&ip={ip}");
        }

        public async Task<ViewDnsPingResponse> GetPing(string ip)
        {
            _logHelper.LogInfo($"HttpViewDnsService - GetGeoIp - ip: {ip}");
            return await _httpClient.GetAsync<ViewDnsPingResponse>($"ping/?output=json&apikey={_settings.ApiKey}&host={ip}");
        }

        public async Task<ViewDnsRdnsResponse> GetRdns(string ip)
        {
            _logHelper.LogInfo($"HttpViewDnsService - GetGeoIp - ip: {ip}");
            return await _httpClient.GetAsync<ViewDnsRdnsResponse>($"reversedns/?output=json&apikey={_settings.ApiKey}&ip={ip}");
        }
    }
}
