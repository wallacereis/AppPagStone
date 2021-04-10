using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace AppPagStone.Stone
{

    /// <summary>
    /// Criação de uma nova venda
    /// </summary>
    [DataContract(Name = "CreateSaleRequest", Namespace = "")]
    public class CreateSaleRequest {

        public CreateSaleRequest() {
            Options = new SaleOptions();
        }

        /// <summary>
        /// Lista de transações de cartão de crédito
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public Collection<CreditCardTransaction> CreditCardTransactionCollection { get; set; }

        /// <summary>
        /// Dados do pedido
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public Order Order { get; set; }

        /// <summary>
        /// Informações opcionais para a criação da venda
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public SaleOptions Options { get; set; }

        /// <summary>
        /// Informações complementares da requisição
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public RequestData RequestData { get; set; }
    }
}