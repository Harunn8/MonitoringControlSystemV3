using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class SystemLogs
    {
        public Guid DeviceId { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
        public LogType LogType { get; set; }
        public Guid UserId { get; set; }
    }

    public enum LogType
    {
        Info = 0,
        Warning = 1,
        Error = 2 ,
        Critical = 3
    }
}
