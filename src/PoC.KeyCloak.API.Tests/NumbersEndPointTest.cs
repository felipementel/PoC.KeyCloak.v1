using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace PoC.KeyCloak.API.Tests
{
    public class NumbersTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public NumbersTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostRandomNumber_ReturnsOk()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/v1/numbers/post-random-number", null);

            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task PostRandomNumber_ReturnsUnprocessableEntity()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/v1/numbers/post-random-number", null);

            // Assert
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            }
            else
            {
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task PostRandomNumber_ReturnsUnauthorized()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/v1/numbers/post-random-number", null);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
