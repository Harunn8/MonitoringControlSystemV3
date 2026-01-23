using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class PagDevices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PagId { get; set; }
        public Guid PagDeviceId { get; set; } = Guid.NewGuid();
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public int Retry {  get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<BaseDeviceModel> Device {  get; set; }
    }
}
