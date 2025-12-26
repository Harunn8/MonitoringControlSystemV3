using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Models
{
    public class ParameterLogsTsModel
    {
        public Guid Id { get; set; }
        public string ParameterSetsName { get; set; }
        public List<Guid> ParameterId { get; set; }
        public List<Guid> DeviceId { get; set; }
        public bool isActive { get; set; }
        public int Interval { get; set; }
    }
}
