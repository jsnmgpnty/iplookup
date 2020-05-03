namespace IpLookupApi.Models
{
    public enum IpLookupErrors
    {
        UnhandledError = 0,
        InvalidResponse = 1,
        RdapResponseError = 2,
        RdnsResponseError = 3,
        PingResponseError = 4,
        GeoIpResponseError = 5
    }
}
