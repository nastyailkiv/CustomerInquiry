using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.Sql.Entities
{
    public sealed class CustomerEntity
    {
        [Key]
        public long CustomerId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string Email { get; set; }

        [Required]
        [Range(0, 9_999_999_999)]
        public long MobileNo { get; set; }

        public ICollection<TransactionEntity> Transactions { get; set; }
    }
}