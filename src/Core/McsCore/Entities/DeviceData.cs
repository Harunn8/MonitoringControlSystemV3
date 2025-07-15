using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class DeviceData
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string Oid { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
