using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace AppPagStone.Stone.Split
{
    public class Split
    {
        public enum AmountSplitMode
        {
            /// <summary>
            /// Porcentagem do valor total da transação, o valor da porcentagem é informado na requisição
            /// </summary>
            percentual,
            /// <summary>
            /// Valor enviado ao recipient é o valor absoluto informado na requisição
            /// </summary>
            absolute
        }

        public enum FeeLiability
        {
            /// <summary>
            /// O custo do MDR fica somente sobre quem faz a transação
            /// </summary>
            Merchant,
            /// <summary>
            /// O custo do MDR é dividido proporcionalmente entre Merchant e recipientes
            /// </summary>
            Shared,
            /// <summary>
            /// O custo do MDR fica somente sobre os recipientes da transação
            /// </summary>
            Recipient
        }

        public string provider { get; set; }
        public string provider_transaction_key { get; set; }
        public float transaction_amount { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AmountSplitMode amount_split_mode { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FeeLiability fee_liability { get; set; }
        public List<SplitData> splits { get; set; }
    }
}
