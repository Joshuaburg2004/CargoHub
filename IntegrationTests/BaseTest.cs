using System.Diagnostics.CodeAnalysis;
using Xunit;
using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
namespace IntegrationTests;
using System;
using System.Net.Http;
using Xunit;

[ExcludeFromCodeCoverage]
public class BaseTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;

    public BaseTest(CustomWebApplicationFactory<Program> factory)
    {
        try
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost:3000");
            _client.DefaultRequestHeaders.Add("API_KEY", "a1b2c3d4e5");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to initialize HttpClient for tests.", ex);
        }
    }
}