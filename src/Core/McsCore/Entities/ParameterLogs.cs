using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    [BsonIgnoreExtraElements]
    public class ParameterLogs
    {
        public Guid Id { get; set; }
        public Guid ParameterId { get; set; }
        public string ParameterName { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string ParameterValue { get; set; }
        public DateTime ParameterTimeStamp { get; set; }
        public string ParameterDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
