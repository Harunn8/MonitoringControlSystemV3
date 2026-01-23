using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Models
{
    public class SnmpCommandModel
    {
        public SnmpDeviceModel SnmpDevice { get; set; }
        public string Oid { get; set; }
        public string Value { get; set; }
    }
}
