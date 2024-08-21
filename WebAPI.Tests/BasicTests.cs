using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace WebApi.Test;

public class BasicTests(WebApplicationFactory<Program> _factory, ITestOutputHelper _output)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Theory]
    [InlineData("/WeatherForecast")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        //_output.WriteLine(await response.Content.ReadAsStringAsync());

        Assert.Equal("text/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}