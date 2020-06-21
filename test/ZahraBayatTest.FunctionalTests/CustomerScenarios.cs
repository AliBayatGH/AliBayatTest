using AliBayatTest.API.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AliBayatTest.FunctionalTests
{
    public class CustomerScenarios
        : IClassFixture<WebApplicationFactory<API.Startup>>
    {
        private readonly WebApplicationFactory<API.Startup> _factory;

        public CustomerScenarios(WebApplicationFactory<API.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/customers")]
        [InlineData("/api/customers/1")]
        public async Task Get_Customers_Return_Success_And_CorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Update_Customer_Return_Success()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var content = new StringContent(BuildCustomer(), UTF8Encoding.UTF8, "application/json");
            var response = await client
                .PutAsync("/api/customers/1", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Add_Customer_When_Its_Not_Unique_InternalServerError_Response()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var content = new StringContent(BuildCustomer(), UTF8Encoding.UTF8, "application/json");
            var response = await client
                .PostAsync("/api/customers", content);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        string BuildCustomer()
        {
            var customer = new CustomerViewModel()
            {
                FirstName = "Ali",
                LastName = "Bayat",
                DateOfBirth = new DateTime(1988, 2, 20),
                PhoneNumber = "+989391818607",
                Email = "ali.bayat.gh@gmail.com",
                BankAccountNumber = "123456"
            };

            return JsonConvert.SerializeObject(customer);
        }
    }
}