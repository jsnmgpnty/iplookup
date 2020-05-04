using System.Threading.Tasks;
using IpLookup.Common;
using IpLookup.Models;
using IpLookup.Services.Http;
using IpLookupApi.Http.Responses;
using IpLookupApi.Http.Settings;
using Newtonsoft.Json;
using Serilog;

namespace IpLookupApi.Http.Services
{
    public class HttpIpProcessorService : IHttpIpProcessorService
    {
        private readonly ILogHelper _logHelper;
        private readonly HttpClientWrapper _httpClient;

        public HttpIpProcessorService(ILogHelper logHelper, ILogger logger, HttpIpProcessorServiceSettings httpSettings, JsonSerializerSettings serializerSettings)
        {
            _logHelper = logHelper;
            _httpClient = new HttpClientWrapper(logger, httpSettings.BaseUrl, httpSettings.TimeoutInMilliseconds, serializerSettings);
        }

        public async Task<IpProcessorResponse> GetGeo(string ip)
        {
            _logHelper.LogInfo($"HttpIpProcessorService - GetGeo - ip: {ip} - host: {_httpClient.BaseAddress}");
            var res = await _httpClient.GetAsync<BaseResponse<GeoIpResponse>>($"ip/{ip}/geo");
            return HandleResponse(res, "GetGeo", ip);
        }

        public async Task<IpProcessorResponse> GetPing(string ip)
        {
            _logHelper.LogInfo($"HttpIpProcessorService - GetPing - ip: {ip} - host: {_httpClient.BaseAddress}");
            var res = await _httpClient.GetAsync<BaseResponse<PingResponse>>($"ip/{ip}/ping");
            return HandleResponse(res, "GetPing", ip);
        }

        public async Task<IpProcessorResponse> GetRdap(string ip)
        {
            _logHelper.LogInfo($"HttpIpProcessorService - GetRdap - ip: {ip} - host: {_httpClient.BaseAddress}");
            var res = await _httpClient.GetAsync<BaseResponse<RdapResponse>>($"ip/{ip}/rdap");
            return HandleResponse(res, "GetRdap", ip);
        }

        public async Task<IpProcessorResponse> GetRdns(string ip)
        {
            _logHelper.LogInfo($"HttpRdapService - GetRdns - ip: {ip} - host: {_httpClient.BaseAddress}");
            var res = await _httpClient.GetAsync<BaseResponse<RdnsResponse>>($"ip/{ip}/rdns");
            return HandleResponse(res, "GetRdns", ip);
        }

        private T HandleResponse<T>(BaseResponse<T> res, string methodName, string ip)
        {
            if (res == null || res.Data == null || !res.IsSuccess)
            {
                _logHelper.LogError($"HttpIpProcessorService - {methodName} - ip: {ip} | {res.Message ?? "Failed to get response"}");
                return default;
            }

            return res.Data;
        }
    }
}
