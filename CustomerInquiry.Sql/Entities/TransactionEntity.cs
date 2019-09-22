using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerInquiry.Shared;

namespace CustomerInquiry.Sql.Entities
{
    public sealed class TransactionEntity
    {
        [Key]
        public long TransactionId { get; set; }

        [Required]
        public DateTime TransactionDateTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        public Status Status { get; set; }

        public long CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
    }
}