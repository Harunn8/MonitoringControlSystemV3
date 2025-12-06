using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Responses
{
    public class ParameterLogTableResponse
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public DateTime ParameterTimeStamp { get; set; }
    }
}
