using System;

namespace CustomerInquiry.Shared
{
    public class TransactionDto
    {
        public long TransactionId { get; set; }

        public DateTime TransactionDateTime { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public Status Status { get; set; }
    }
}