using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IpProcessorApi.Http.Responses
{
    public class RdapResponse
    {
        [JsonProperty("rdapConformance")]
        public List<string> RdapConformance { get; set; }

        [JsonProperty("notices")]
        public List<RdapRemark> Notices { get; set; }

        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("startAddress")]
        public string StartAddress { get; set; }

        [JsonProperty("endAddress")]
        public string EndAddress { get; set; }

        [JsonProperty("ipVersion")]
        public string IpVersion { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("parentHandle")]
        public string ParentHandle { get; set; }

        [JsonProperty("events")]
        public List<RdapEvent> Events { get; set; }

        [JsonProperty("links")]
        public List<RdapLink> Links { get; set; }

        [JsonProperty("port43")]
        public string Port43 { get; set; }

        [JsonProperty("status")]
        public List<string> Status { get; set; }

        [JsonProperty("objectClassName")]
        public string ObjectClassName { get; set; }

        [JsonProperty("cidr0_cidrs")]
        public List<Cidr> Cidr0Cidrs { get; set; }
    }

    public class RdapLink
    {
        [JsonProperty("value")]
        public Uri Value { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public class RdapRemark
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public List<string> Description { get; set; }

        [JsonProperty("links")]
        public List<RdapLink> Links { get; set; }
    }

    public class RdapEvent
    {
        [JsonProperty("eventAction")]
        public string EventAction { get; set; }

        [JsonProperty("eventDate")]
        public DateTime EventDate { get; set; }
    }

    public class RdapEntity
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("remarks", NullValueHandling = NullValueHandling.Ignore)]
        public List<RdapRemark> Remarks { get; set; }

        [JsonProperty("links")]
        public List<RdapLink> Links { get; set; }

        [JsonProperty("events")]
        public List<RdapEvent> Events { get; set; }

        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RdapEntity> Entities { get; set; }

        [JsonProperty("port43")]
        public string Port43 { get; set; }

        [JsonProperty("objectClassName")]
        public string ObjectClassName { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Status { get; set; }
    }

    public class Cidr
    {
        [JsonProperty("v4prefix")]
        public string V4Prefix { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }
    }
}
