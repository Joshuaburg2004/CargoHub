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
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("http://localhost:3000");
        _client.DefaultRequestHeaders.Add("API_KEY", "a1b2c3d4e5");
    }
}