using System;
using System.Collections.Generic;

namespace McsCore.Entities
{
    public class SnmpDevice
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public SnmpVersion SnmpVersion { get; set; }
        public int Timeout { get; set; }
        public int Retry { get; set; }
        public string Version { get; set; }
        public string ReadCommunity { get; set; }
        public string WriteCommunity { get; set; }
        public List<ParameterModel> Parameters { get; set; }

    }

    public enum SnmpVersion
    {
        V1 = 1,
        V2 = 2
    }
}
