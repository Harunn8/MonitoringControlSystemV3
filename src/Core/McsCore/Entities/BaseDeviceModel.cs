using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class BaseDeviceModel
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Category { get; set; }
        public string DeviceName { get; set; }
        public string Version { get; set; }
        public string CommunicationData { get; set; }
        public List<Alarms> Alarms { get; set; }
    }

    public enum CommunicationType
    {
         SNMP,
         TCP
    }
}
