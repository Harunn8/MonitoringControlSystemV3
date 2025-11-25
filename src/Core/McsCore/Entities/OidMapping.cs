using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class OidMapping
    {
        [Key]
        public Guid ParameterId { get; set; }
        public string Oid { get; set; }
        public string ParameterName { get; set; }
    }
}
