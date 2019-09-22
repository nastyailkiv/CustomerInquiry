using System;
using CustomerInquiry.Shared;
using CustomerInquiry.Sql;
using CustomerInquiry.Sql.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomerInquiry.Tests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    public partial class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customer.Any())
            {
                return; // DB has been seeded
            }

            var customer = new CustomerEntity {Name = "Lorem", Email = "email1@gmail.com", MobileNo = 2135000001};
            context.Customer.Add(customer);
            context.SaveChanges();

            var transaction = new TransactionEntity
            {
                Amount = 0.2m, CurrencyCode = "Usd", Status = Status.Success, TransactionDateTime = DateTime.UtcNow,
                CustomerId = 1
            };
            
            context.Transaction.Add(transaction);
            context.SaveChanges();
        }
    }
}