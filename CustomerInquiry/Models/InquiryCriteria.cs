using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.Models
{
    public class InquiryCriteria
    {
        [Range(0, 9_999_999_999)]
        public long? CustomerId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(25)]
        public string Email { get; set; }
    }
}