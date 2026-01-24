using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Responses
{
    public class PagDeviceResponses
    {
        public Guid Id {  get; set; }
        public Guid PagId { get; set; }
        public Guid DeviceId { get; set; }
        public string PagDeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public int Retry { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public string CommunicationData { get; set; }
        public bool IsActive { get; set; }
    }
}
