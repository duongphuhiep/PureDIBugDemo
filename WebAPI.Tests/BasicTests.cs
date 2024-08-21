using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Test;

public class BasicTests(WebApplicationFactory<Program> _factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Theory]
    [InlineData("/Counter")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        {// first request to the webapi
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Assert.Equal("1", content);
        }
        {// second request to the webapi
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            /*
             * I expected PureDI to create a new CounterService for this request
             * because the CounterService was registered as "Scoped".
             * but it is not, the old CounterService created since the first request is re-used for this second request
             */
            string content = await response.Content.ReadAsStringAsync();

            Assert.Equal("1", content); //test failed here, actual value is "2"
        }
    }
}