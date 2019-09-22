using System.Net.Http;
using System.Threading.Tasks;
using CustomerInquiry.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CustomerInquiry.Tests.IntegrationTests
{
    public class CustomersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CustomersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/Customers?CustomerId=1")]
        [InlineData("/api/Customers?Email=email1@gmail.com")]
        [InlineData("/api/Customers?CustomerId=1&Email=email1@gmail.com")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");

            var message = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(message);
            customer.Email.Should().Be("email1@gmail.com");
            customer.Id.Should().Be(1);
            customer.Transactions.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("/api/Customers?CustomerId=55")]
        [InlineData("/api/Customers?Email=email12@gmail.com")]
        [InlineData("/api/Customers?CustomerId=1&Email=email12@gmail.com")]
        [InlineData("/api/Customers?CustomerId=55&Email=email1@gmail.com")]
        [InlineData("/api/Customers?CustomerId=55&Email=email12@gmail.com")]
        public async Task Get_EndpointsReturnNoContent(string url)
        {
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Theory]
        [InlineData("/api/Customers")]
        [InlineData("/api/Customers?Email=qwk2l1#1")]
        [InlineData("/api/Customers?CustomerId=6546548916518189189189")]
        [InlineData("/api/Customers?Email=email1email1email1email1email1@gmail.com")]
        public async Task Get_EndpointsReturnBadRequest(string url)
        {
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}