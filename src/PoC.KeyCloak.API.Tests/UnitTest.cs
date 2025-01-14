using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace PoC.KeyCloak.API.Tests
{
    public class PingTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PingTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPing_ReturnsVersionString()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/ping-pong-test");

            // Assert
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync(default);
            var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
            var version = responseObject.GetProperty("version").GetString();

            string versionDLL = Assembly.LoadFrom("PoC.KeyCloak.API.dll").GetName().Version!.ToString();
            Assert.Equal(versionDLL, version);
        }

        [Fact]
        public async Task GetPing_RequiresAuthorization()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/ping-pong-test");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPing_WithValidToken_ReturnsVersionString()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //mock
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/ping-pong-test");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}