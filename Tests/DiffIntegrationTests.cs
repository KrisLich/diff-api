using DiffAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace DiffAPI.Tests
{
    public class DiffIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public DiffIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot("../../../wwwroot");
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task UploadLeftData_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var id = "123";
            var data = new DataRequestModel { Data = "SGVsbG8=" };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/v1/diff/{id}/left", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UploadRightData_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var id = "123";
            var data = new DataRequestModel { Data = "AQABAQ==" };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/v1/diff/{id}/right", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
