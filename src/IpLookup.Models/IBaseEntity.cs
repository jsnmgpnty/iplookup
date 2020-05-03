using MongoDB.Bson;
using System;

namespace IpLookup.Models
{
    public interface IBaseEntity
    {
        string Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime ModifiedDate { get; set; }

        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }
    }
}
