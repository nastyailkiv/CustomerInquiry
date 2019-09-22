using System.Collections.Generic;

namespace CustomerInquiry.Shared
{
    public class CustomerDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long MobileNo { get; set; }

        public ICollection<TransactionDto> Transactions { get; set; }
    }
}