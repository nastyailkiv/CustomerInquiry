using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerInquiry.Models;
using CustomerInquiry.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        private static readonly MapperConfiguration Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.Take(5).ToList()));
                cfg.CreateMap<TransactionDto, Transaction>()
                    .ForMember(dest => dest.TransactionDateTime,
                        opt => opt.MapFrom(src => src.TransactionDateTime.ToString("dd/MM/yyyy HH:mm")));
            }
        );

        public CustomersController(IRepository repository)
        {
            _repository = repository;
            _mapper = Configuration.CreateMapper();
        }

        /// <summary>
        /// Returns customer by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Customer>> Get([FromQuery] InquiryCriteria criteria)
        {
            if (criteria.Email == null && criteria.CustomerId == null)
            {
                return BadRequest("One of the parameters (email or customer id) should be set");
            }

            Task<CustomerDto> customerTask;
            if (criteria.Email != null && criteria.CustomerId != null)
            {
                customerTask = _repository.GetCustomer(criteria.CustomerId.Value, criteria.Email);
            }
            else if (criteria.CustomerId != null)
            {
                customerTask = _repository.GetCustomer(criteria.CustomerId.Value);
            }
            else
            {
                customerTask = _repository.GetCustomer(criteria.Email);
            }

            var customerDto = await customerTask;

            if (customerDto == null)
            {
                return NotFound();
            }

            var customer = _mapper.Map<Customer>(customerDto);
            return customer;
        }
    }
}