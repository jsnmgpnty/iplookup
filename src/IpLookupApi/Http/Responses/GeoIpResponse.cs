namespace IpLookupApi.Http.Responses
{
    public class GeoIpResponse : IpProcessorResponse
    {
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GmtOffset { get; set; }
        public string DstOffset { get; set; }
    }
}