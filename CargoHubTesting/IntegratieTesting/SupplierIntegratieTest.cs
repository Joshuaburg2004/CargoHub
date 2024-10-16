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
    [TestCaseOrderer("MyTests.PriorityOrderer", "CargoHubTesting")]

    public class SupplierIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid supplier1Id;
        public static Supplier supplierafterupdate;

        public SupplierIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllSuppliersEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateSupplier()
        {
            HttpResponseMessage response = await _client.PostAsync("/api/v1/suppliers", new StringContent("{\"Code\":\"SUP123\",\"Name\":\"Supplier Name\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var supplierid = JsonSerializer.Deserialize<Guid>(responseContent);

            Xunit.Assert.NotNull(supplierid);
            Xunit.Assert.NotEqual(Guid.Empty, supplierid);
            supplier1Id = supplierid;
        }

        [Fact, TestPriority(2)]
        public async Task GetAllSuppliers()
        {
            Console.Error.WriteLine("supplier1Id: " + supplier1Id);
            HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        [Fact, TestPriority(3)]
        public async Task GetSupplierById()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/suppliers?id={supplier1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var supplier = JsonSerializer.Deserialize<Supplier>(responseContent);
            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(supplier1Id, supplier.Id);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateSupplier()
        {
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/suppliers?id={supplier1Id}", new StringContent("{\"Code\":\"SUP12345\",\"Name\":\"Supplier Name Updated\",\"Address\":\"123 Main St\",\"AddressExtra\":\"Suite 100\",\"City\":\"Rotterdam\",\"ZipCode\":\"3011AA\",\"Province\":\"South Holland\",\"Country\":\"Netherlands\",\"ContactName\":\"John Doe\",\"PhoneNumber\":\"+31 10 123 4567\",\"Reference\":\"REF123\",\"CreatedAt\":\"2023-10-01T12:00:00Z\",\"UpdatedAt\":\"2023-10-01T12:00:00Z\"}", System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            supplierafterupdate = JsonSerializer.Deserialize<Supplier>(responseContent);

            Xunit.Assert.NotNull(supplierafterupdate);
            Xunit.Assert.NotEqual(Guid.Empty, supplierafterupdate.Id);
            Xunit.Assert.Equal("Supplier Name Updated", supplierafterupdate.Name);
        }

        [Fact, TestPriority(5)]
        public async Task GetSupplierByIdAfterUpdate()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/suppliers?id={supplierafterupdate.Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var supplier = JsonSerializer.Deserialize<Supplier>(responseContent);
            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(supplierafterupdate.Id, supplier.Id);
            Xunit.Assert.Equal("Supplier Name Updated", supplier.Name);
        }

        [Fact, TestPriority(6)]
        public async Task DeleteSupplier()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/suppliers?id={supplierafterupdate.Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetAllSuppliersAfterDelete()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/suppliers/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("[]", responseContent);
        }
    }
}