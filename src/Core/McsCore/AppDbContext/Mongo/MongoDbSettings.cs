using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Mongo
{
    public class MongoDbSettings
    {
        public string DatabaseName { get; set; }
        public Collections Collections { get; set; }
    }

    public class Collections
    {
        public string Alarms { get; set; }
        public string ActiveAlarms { get; set; }
        public string HistoricalAlarms { get; set; }
        public string SystemLogs { get; set; }
        public string UserLogs { get; set; }
        public string ParameterLogs { get; set; }
    }
}
