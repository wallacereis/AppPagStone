using System.Collections.Generic;

namespace AppPagStone.Stone.Split
{

    public class SplitResponse
    {
        public Data data { get; set; }
        public bool success { get; set; }
        public List<OperationReport> operation_report { get; set; }
    }

    public class Data
    {
        public string split_key { get; set; }
        public string split_status { get; set; }
    }
}
