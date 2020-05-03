using System;
using System.Collections.Generic;

namespace IpLookupApi.Http.Responses
{
    public class RdapResponse : IpProcessorResponse
    {
        public List<string> RdapConformance { get; set; }
        public List<RdapRemark> Notices { get; set; }
        public string Handle { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public string IpVersion { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ParentHandle { get; set; }
        public List<RdapEvent> Events { get; set; }
        public List<RdapLink> Links { get; set; }
        public string Port43 { get; set; }
        public List<string> Status { get; set; }
        public string ObjectClassName { get; set; }
        public List<Cidr> Cidr0Cidrs { get; set; }
    }

    public class RdapLink
    {
        public Uri Value { get; set; }
        public string Rel { get; set; }
        public string Type { get; set; }
        public Uri Href { get; set; }
    }

    public class RdapRemark
    {
        public string Title { get; set; }
        public List<string> Description { get; set; }
        public List<RdapLink> Links { get; set; }
    }

    public class RdapEvent
    {
        public string EventAction { get; set; }
        public DateTime EventDate { get; set; }
    }

    public class RdapEntity
    {
        public string Handle { get; set; }
        public List<string> Roles { get; set; }
        public List<RdapRemark> Remarks { get; set; }
        public List<RdapLink> Links { get; set; }
        public List<RdapEvent> Events { get; set; }
        public List<RdapEntity> Entities { get; set; }
        public string Port43 { get; set; }
        public string ObjectClassName { get; set; }
        public List<string> Status { get; set; }
    }

    public class Cidr
    {
        public string V4Prefix { get; set; }
        public long Length { get; set; }
    }
}
