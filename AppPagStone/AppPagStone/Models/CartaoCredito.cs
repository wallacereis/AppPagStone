using AppPagStone.Stone.EnumTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppPagStone.Models
{
    public class CartaoCredito
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string Validade { get; set; }
        public string CVV { get; set; }
        public string Bandeira { get; set; }
        public decimal ValorVenda { get; set; }
        public int NumeroParcelas { get; set; }
        public string OrderReference { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CreditCardBrandEnum CreditCardBrand { get; set; }
    }
}
