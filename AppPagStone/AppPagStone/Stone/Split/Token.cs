using AppPagStone.Stone.Split;
using System.Collections.Generic;

namespace AppPagStone.Stone
{
    public class Token
    {
        public TokenData data { get; set; }
        public bool success { get; set; }
        public List<OperationReport> operation_report { get; set; }
    }
}
