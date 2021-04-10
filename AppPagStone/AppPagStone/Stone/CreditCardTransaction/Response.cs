using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPagStone.Stone.Data
{
    public class Response
    {
        public object ErrorReport { get; set; }
        public int InternalTime { get; set; }
        public string MerchantKey { get; set; }
        public string RequestKey { get; set; }
        public object[] BoletoTransactionResultCollection { get; set; }
        public string BuyerKey { get; set; }
        public Creditcardtransactionresultcollection[] CreditCardTransactionResultCollection { get; set; }
        public Orderresult OrderResult { get; set; }
    }

    public class Orderresult
    {
        public DateTime CreateDate { get; set; }
        public string OrderKey { get; set; }
        public string OrderReference { get; set; }
    }

    public class Creditcardtransactionresultcollection
    {
        public string AcquirerMessage { get; set; }
        public string AcquirerName { get; set; }
        public string AcquirerReturnCode { get; set; }
        public string AffiliationCode { get; set; }
        public int AmountInCents { get; set; }
        public string AuthorizationCode { get; set; }
        public int AuthorizedAmountInCents { get; set; }
        public int CapturedAmountInCents { get; set; }
        public DateTime CapturedDate { get; set; }
        public Creditcard CreditCard { get; set; }
        public string CreditCardOperation { get; set; }
        public string CreditCardTransactionStatus { get; set; }
        public object DueDate { get; set; }
        public int ExternalTime { get; set; }
        public string PaymentMethodName { get; set; }
        public object RefundedAmountInCents { get; set; }
        public bool Success { get; set; }
        public string TransactionIdentifier { get; set; }
        public string TransactionKey { get; set; }
        public string TransactionKeyToAcquirer { get; set; }
        public string TransactionReference { get; set; }
        public string UniqueSequentialNumber { get; set; }
        public object VoidedAmountInCents { get; set; }
    }

    public class Creditcard
    {
        public string CreditCardBrand { get; set; }
        public string InstantBuyKey { get; set; }
        public bool IsExpiredCreditCard { get; set; }
        public string MaskedCreditCardNumber { get; set; }
    }
}
