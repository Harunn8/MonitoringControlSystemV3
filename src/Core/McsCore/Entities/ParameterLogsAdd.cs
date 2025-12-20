using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class ParameterLogsAdd
    {
        public Guid Id { get; set; }
        public string ParameterSetsName { get; set; }
        public List<Guid> ParameterId { get; set; }
        public List<Guid> DeviceId { get; set; }
        public bool isActive { get; set; }
    }
}
