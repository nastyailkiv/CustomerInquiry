using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Shared;
using CustomerInquiry.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerInquiry.Sql.Repositories
{
    public class SqlRepository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private static readonly MapperConfiguration Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerEntity, CustomerDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));
                cfg.CreateMap<TransactionEntity, TransactionDto>();
            }
        );

        public SqlRepository(ApplicationDbContext context)
        {
            _context = context;
            _mapper = Configuration.CreateMapper();
        }

        public Task<CustomerDto> GetCustomer(long customerId)
        {
            return GetCustomer(x => x.CustomerId == customerId);
        }

        public Task<CustomerDto> GetCustomer(string email)
        {
            return GetCustomer(x => x.Email == email);
        }

        public Task<CustomerDto> GetCustomer(long customerId, string email)
        {
            return GetCustomer(x => x.CustomerId == customerId && x.Email == email);
        }

        private async Task<CustomerDto> GetCustomer(Expression<Func<CustomerEntity, bool>> expression)
        {
            var customerEntity = await _context.Customer
                .Include(x => x.Transactions)
                .SingleOrDefaultAsync(expression);

            if (customerEntity == null)
            {
                return null;
            }

            var customer = _mapper.Map<CustomerDto>(customerEntity);
            return customer;
        }
    }
}