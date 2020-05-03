using System.Collections.Generic;
using Newtonsoft.Json;

namespace IpProcessorApi.Http.Responses
{
    public class ViewDnsPingResponse : BaseViewDnsResponse<ViewDnsPingResponseReplies>
    {
    }

    public class ViewDnsPingResponseReply
    {
        [JsonProperty("rtt")]
        public string Rtt { get; set; }
    }

    public class ViewDnsPingResponseReplies
    {
        [JsonProperty("replys")]
        public List<ViewDnsPingResponseReply> Replys { get; set; }
    }
}
