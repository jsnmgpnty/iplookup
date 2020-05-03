using Newtonsoft.Json;

namespace IpProcessorApi.Http.Responses
{
    public class ViewDnsRdnsResponse : BaseViewDnsResponse<ViewDnsRdns>
    {
    }

    public partial class ViewDnsRdns
    {
        [JsonProperty("rdns")]
        public string Rdns { get; set; }
    }
}
