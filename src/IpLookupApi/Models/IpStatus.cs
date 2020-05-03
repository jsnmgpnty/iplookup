using IpLookupApi.Http.Responses;

namespace IpLookupApi.Models
{
    public class IpStatus
    {
        public RdapResponse Rdap { get; set; }
        public PingResponse Ping { get; set; }
        public GeoIpResponse GeoIp { get; set; }
        public RdnsResponse Rdns { get; set; }
    }
}
