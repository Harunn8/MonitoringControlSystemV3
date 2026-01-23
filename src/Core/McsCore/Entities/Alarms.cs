using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class Alarms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public Guid ParameterId { get; set; }
        public string ParameterName { get; set; }
        public bool IsActive { get; set; }
        public bool IsFixed { get; set; } 
        public string Description { get; set; }
        public string Threshold { get; set; }
        public string Condition { get; set; }
        public Severity Severity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public enum Severity
    {
        Information = 0,
        Warning = 1,
        Low = 2,
        Error = 3,
        Medium = 4,
        High = 5,
        CriticalHigh = 6
    }
}
