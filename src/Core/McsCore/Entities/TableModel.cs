using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class TableModel
    {
        public string Sort { get; set; }
        public string SortType { get; set; }
        public string Search { get; set; }
        public string SpeacialSearchField { get; set; }
        public int RowSize { get; set; }
        public int ActivePage { get; set; }
    }

    public class ParameterLogSpeacialSearch
    {
        public Guid? DevicecId { get; set; }
        public Guid? ParameterId { get; set; }
        public DateTime? ParameterTimeStamp { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
    }
}
