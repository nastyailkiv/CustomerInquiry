using System.Collections.Generic;

namespace CustomerInquiry.Models
{
    public class Customer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long MobileNo { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}