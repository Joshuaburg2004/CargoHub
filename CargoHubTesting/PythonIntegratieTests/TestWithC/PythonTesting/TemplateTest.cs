using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

public class TemplateTest
{
    private readonly HttpClient _client;

    public TemplateTest()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:3000");
        _client.DefaultRequestHeaders.Add("API_KEY", "a1b2c3d4e5");
    }

    [Fact]
    public async Task TestGetClients()
    {
        // Arrange
        var requestUri = "/api/v1/clients";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseContent);
        // Additional assertions based on expected response content
    }
}