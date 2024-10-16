using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;


public class SupplierIntegratieTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;
    public Guid supplier1Id;

    public SupplierIntegratieTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CargoHubContext>();
            dbContext.Database.Migrate();
        }
    }

    [Fact]
    public async Task GetAllSuppliersEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateSupplier()
    {
        HttpResponseMessage response = await _client.PostAsync("/api/v1/suppliers", new StringContent("{\"Code\":\"SUP123\",\"Name\":\"Supplier Name\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
        Console.Error.WriteLine(response.Content.ReadAsStringAsync().Result);
        supplier1Id = JsonSerializer.Deserialize<Guid>(response.Content.ReadAsStringAsync().Result);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllSuppliers()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSupplierById()
    {
        Console.Error.WriteLine(supplier1Id);
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/suppliers?id={supplier1Id}");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}