using Newtonsoft.Json;

namespace IpProcessorApi.Http.Responses
{
    public class ViewDnsGeoIpResponse : BaseViewDnsResponse<ViewDnsGeoIpResponseLocation>
    {
    }

    public class ViewDnsGeoIpResponseLocation
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("gmt_offset")]
        public string GmtOffset { get; set; }

        [JsonProperty("dst_offset")]
        public string DstOffset { get; set; }
    }
}
