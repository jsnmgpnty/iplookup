using Newtonsoft.Json;

namespace IpProcessorApi.Http.Responses
{ 
    public class BaseViewDnsResponse<T>
    {
        [JsonProperty("query")]
        public ViewDnsResponseQuery Query { get; set; }

        [JsonProperty("response")]
        public T Response { get; set; }
    }

    public class ViewDnsResponseQuery
    {
        [JsonProperty("tool")]
        public string Tool { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }
    }
}
