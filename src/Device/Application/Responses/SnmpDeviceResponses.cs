
using DeviceApplication.Models;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class SnmpDeviceResponses : SnmpDeviceModel
    {
        public SnmpVersion SnmpVersion { get; set; }
        public string ReadCommunity { get; set; }
        public string WriteCommunity { get; set; }
        public PagDevices PagDevice { get; set; }
    }
}
