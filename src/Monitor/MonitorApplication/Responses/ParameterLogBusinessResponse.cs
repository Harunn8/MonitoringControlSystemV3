using System;
using System.Collections.Generic;

namespace MonitorApplication.Responses
{
    public class ParameterLogBusinessResponse
    {
        public Guid Id { get; set; }
        public string ParameterSetsName { get; set; }
        public List<Guid> ParameterId { get; set; }
        public Guid DeviceId { get; set; }
        public bool isActive { get; set; }
    }
}