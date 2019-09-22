using System;
using System.Threading.Tasks;
using CustomerInquiry.Shared;
using CustomerInquiry.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerInquiry.Sql
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (await context.Customer.AnyAsync())
            {
                return; // DB has been seeded
            }

            var customers = new[]
            {
                new CustomerEntity
                {
                    Name = "Lorem", Email = "email1@gmail.com", MobileNo = 2135000001
                },
                new CustomerEntity
                {
                    Name = "Ipsum", Email = "email2@gmail.com", MobileNo = 2135000002
                },
                new CustomerEntity
                {
                    Name = "Simply", Email = "email3@gmail.com", MobileNo = 2135000003,
                }
            };
            await context.Customer.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            var transactions = new[]
            {
                new TransactionEntity()
                {
                    Amount = 0.2m, CurrencyCode = "Usd", Status = Status.Success, TransactionDateTime = DateTime.UtcNow,
                    CustomerId = 1
                },
                new TransactionEntity
                {
                    Amount = 21.23m, CurrencyCode = "Jpy", Status = Status.Failed,
                    TransactionDateTime = DateTime.UtcNow, CustomerId = 1
                },
                new TransactionEntity
                {
                    Amount = 5.22m, CurrencyCode = "Thb", Status = Status.Failed, TransactionDateTime = DateTime.UtcNow,
                    CustomerId = 2
                },
                new TransactionEntity
                {
                    Amount = 5.22m, CurrencyCode = "Sgb", Status = Status.Canceled,
                    TransactionDateTime = DateTime.UtcNow, CustomerId = 2
                },
                new TransactionEntity
                {
                    Amount = 5.22m, CurrencyCode = "Aud", Status = Status.Success,
                    TransactionDateTime = DateTime.UtcNow, CustomerId = 3
                },
                new TransactionEntity
                {
                    Amount = 5.22m, CurrencyCode = "Uah", Status = Status.Canceled,
                    TransactionDateTime = DateTime.UtcNow, CustomerId = 3
                },
            };
            await context.Transaction.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }
    }
}