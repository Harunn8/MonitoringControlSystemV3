using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Models
{
    public class PagDeviceAddModel
    {
        public string PagDeviceName { get; set; }
        public Guid PagId { get; set; }
        public Guid DeviceId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public int Retry { get; set; }
        public bool IsActive { get; set; }
    }
}
