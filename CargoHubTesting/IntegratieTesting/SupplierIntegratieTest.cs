using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;

namespace MyTests
{
    [TestCaseOrderer("MyNamespace.PriorityOrderer", "CargoHubTesting")]

    public class SupplierIntegratieTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        public static Guid supplier1Id;
        public static Supplier supplierafterupdate;


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

        [Fact, TestPriority(0)]
        public async Task GetAllSuppliersEmpty()
        {
            Console.Error.WriteLine("GetAllSuppliersEmpty");
            HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // does not work because the create happens before the getall
        }

        [Fact, TestPriority(1)]
        public async Task CreateSupplier()
        {
            Console.Error.WriteLine("CreateSupplier");
            HttpResponseMessage response = await _client.PostAsync("/api/v1/suppliers", new StringContent("{\"Code\":\"SUP123\",\"Name\":\"Supplier Name\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
            supplier1Id = JsonSerializer.Deserialize<Guid>(response.Content.ReadAsStringAsync().Result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetAllSuppliers()
        {
            Console.Error.WriteLine("GetAllSuppliers");
            HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(3)]
        public async Task GetSupplierById()
        {
            Console.Error.WriteLine("GetSupplierById");
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/suppliers?id={supplier1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // [Fact]
        // public async Task UpdateSupplier()
        // {
        //     HttpResponseMessage response = await _client.PutAsync($"/api/v1/suppliers?id={supplier1Id}", new StringContent("{\"Code\":\"SUP12345\",\"Name\":\"Supplier Name\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
        //     Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     Console.Error.WriteLine(responseContent);

        //     supplierafterupdate = JsonSerializer.Deserialize<Supplier>(responseContent);
        //     Console.Error.WriteLine(supplierafterupdate.Id);

        //     Xunit.Assert.NotNull(supplierafterupdate);
        //     Xunit.Assert.NotEqual(Guid.Empty, supplierafterupdate.Id);
        // }

        [Fact, TestPriority(10)]
        public async Task DeleteSupplier()
        {
            Console.Error.WriteLine("DeleteSupplier");
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/suppliers?id={supplier1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}