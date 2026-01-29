using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Models
{
    public class SnmpDeviceAddModel : BaseDeviceModel
    {
        public SnmpVersion SnmpVersion { get; set; }
        public string ReadCommunity {  get; set; }
        public string WriteCommunity { get; set; }
        public List<ParameterModel> Parameters { get; set; }
    }

    public enum SnmpVersion
    {
        V1 = 1,
        V2 = 2
    }

    public class ParameterModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Oid { get; set; }
    }
}
