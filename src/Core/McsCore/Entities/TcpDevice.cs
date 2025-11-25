using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class TcpDevice
    {
        public Guid PagId { get; set; }
        public Guid Id { get; set; } = new Guid();
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public List<string> TcpFormat { get; set; }
        public List<TcpData> TcpData { get; set; }
    }

    public class TcpData
    {
        [Key]
        public Guid ParameterId { get; set; } = new Guid();
        public string Request { get; set; }
        public string ParameterName { get; set; }
    }
}
