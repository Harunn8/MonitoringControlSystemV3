using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class SnmpDevice
    {
        public Guid Id { get; set; } = new Guid();
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public SnmpVersion SnmpVersion { get; set; }
        public int Timeout { get; set; }
        public int Retry { get; set; }
        public string Version { get; set; }
        public List<OidMapping> OidList { get; set; }

    }

    public class OidMapping
    {
        [Key]
        public Guid ParameterId { get; set; } = new Guid();
        public string Oid { get; set; }
        public string ParameterName { get; set; }
    }

    public enum SnmpVersion
    {
        V1 = 1,
        V2 = 2
    }
}
