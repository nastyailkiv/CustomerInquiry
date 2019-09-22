using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CustomerInquiry.Shared
{
    public interface IRepository
    {
        [ItemCanBeNull]
        Task<CustomerDto> GetCustomer(long customerId);

        [ItemCanBeNull]
        Task<CustomerDto> GetCustomer(string email);

        [ItemCanBeNull]
        Task<CustomerDto> GetCustomer(long customerId, string email);
    }
}