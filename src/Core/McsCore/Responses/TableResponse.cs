using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Responses
{
    public class TableResponse
    {
        public string Sort { get; set; }
        public string SortType { get; set; }
        public string Search { get; set; }
        public string SpeacialSearchField { get; set; }
        public int PageSize { get; set; }
        public int RowSize { get; set; }
        public int ActivePage { get; set; }
        public int TotalDataCount { get; set; }
        public object Data { get; set; }
    }
}
