using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace AppPagStone.Stone {

    /// <summary>
    /// Resposta da criação de uma nova venda
    /// </summary>
    [DataContract(Name = "CreateSaleResponse", Namespace = "")]
    public class CreateSaleResponse : BaseResponse {

        /// <summary>
        /// Lista de transações de cartão de crédito
        /// </summary>
        [DataMember]
        public Collection<CreditCardTransactionResult> CreditCardTransactionResultCollection { get; set; }

        /// <summary>
        /// Dados de retorno do pedido
        /// </summary>
        [DataMember]
        public OrderResult OrderResult { get; set; }

        /// <summary>
        /// Chave do comprador. Utilizada para identificar um comprador no gateway
        /// </summary>
        [DataMember]
        public virtual Guid BuyerKey { get; set; }
    }
}