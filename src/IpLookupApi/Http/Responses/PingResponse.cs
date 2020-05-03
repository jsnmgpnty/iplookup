using System.Collections.Generic;

namespace IpLookupApi.Http.Responses
{
    public class PingResponseReply
    {
        public string Rtt { get; set; }
    }

    public class PingResponse : IpProcessorResponse
    {
        public List<PingResponseReply> Replys { get; set; }
    }
}
