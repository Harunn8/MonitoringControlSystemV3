using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Models
{
    public class TcpDeviceAddModel : TcpDeviceModel
    {
        public List<string> TcpFormat { get; set; }
        public List<TcpCommunicationData> TcpCommunicationData { get; set; }
    }

    public class TcpCommunicationData
    {
        [Key]
        public Guid ParameterId { get; set; } = new Guid();
        public string Request { get; set; }
        public string ParameterName { get; set; }
    }
}
