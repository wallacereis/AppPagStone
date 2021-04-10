using System.Collections.Generic;

namespace AppPagStone.Stone.Split
{
    public class RecipientResponse
    {
        public RecipientData data { get; set; }
        public bool success { get; set; }
        public List<RecipientOperationReport> operation_report { get; set; }
    }

    public class RecipientData
    {
        public string recipient_name { get; set; }
        public string recipient_key { get; set; }
    }

    public class RecipientOperationReport
    {
        public string property { get; set; }
        public string message { get; set; }
    }
}
