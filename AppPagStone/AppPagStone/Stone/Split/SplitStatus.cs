using System;
using System.Collections.Generic;

namespace AppPagStone.Stone.Split
{
    public class SplitStatus
    {
        public DataObj data { get; set; }
        public bool success { get; set; }
        public List<OperationReport> operation_report { get; set; }
    }

    public class DataObj
    {
        public string split_key { get; set; }
        public string provider { get; set; }
        public string merchant_name { get; set; }
        public string provider_transaction_key { get; set; }
        public float transaction_amount { get; set; }
        public string amount_split_mode { get; set; }
        public string split_status_name { get; set; }
        public string fee_liability { get; set; }
        public DateTime created_at { get; set; }
        public List<SplitData> splits { get; set; }
    }
}
