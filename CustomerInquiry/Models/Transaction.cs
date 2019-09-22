using CustomerInquiry.Shared;

namespace CustomerInquiry.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        public string TransactionDateTime { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public Status Status { get; set; }
    }
}