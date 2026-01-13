using System;
using System.Collections.Generic;

namespace MonitorApplication.Responses
{
    public class ParameterLogBusinessResponse
    {
        public List<Guid> ParameterIds { get; set; }
        public bool IsActive { get; set; }
    }
}