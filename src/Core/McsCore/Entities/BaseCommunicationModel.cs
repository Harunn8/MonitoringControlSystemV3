using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class BaseCommunicationModel
    {
        public Guid PagId { get; set; }
        public List<ParameterModel> Parameters { get; set; }
    }
}
