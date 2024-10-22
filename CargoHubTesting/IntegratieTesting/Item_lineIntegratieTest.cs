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

    public class Item_lineIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid itemline1Id;
        public static Item_line itemlineafterupdate;

        public Item_lineIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllItemlinesEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemLines/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateItemline()
        {
            HttpResponseMessage response = await _client.PostAsync("/api/v1/itemLines", new StringContent("{\"Name\":\"Item Line Name\",\"Description\":\"Item Line Description\"}", System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var itemlineid = JsonSerializer.Deserialize<Guid>(responseContent);
            itemline1Id = itemlineid;

            Xunit.Assert.NotEqual(Guid.Empty, itemlineid);
        }

        [Fact, TestPriority(2)]
        public async Task GetAllItemlines()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemLines/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        [Fact, TestPriority(3)]
        public async Task GetItemline()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/itemLines/getbyid?id={itemline1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Item Line Name", responseContent);
            Xunit.Assert.Contains("Item Line Description", responseContent);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateItemline()
        {
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/itemLines?id={itemline1Id}", new StringContent("{\"Name\":\"Item line Name Updated\",\"Description\":\"Item Line Description\"}", System.Text.Encoding.UTF8, "application/json"));
            HttpResponseMessage responseget = await _client.GetAsync($"/api/v1/itemLines/getbyid?id={itemline1Id}");

            var content = await responseget.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            itemlineafterupdate = JsonSerializer.Deserialize<Item_line>(content, options);

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal(HttpStatusCode.OK, responseget.StatusCode);
            Xunit.Assert.Equal("Item line Name Updated", itemlineafterupdate.Name);
            Xunit.Assert.Equal(itemline1Id, itemlineafterupdate.Id);
        }


        [Fact, TestPriority(6)]
        public async Task DeleteItemline()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/itemLines?id={itemlineafterupdate.Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(7)]
        public async Task GetItemlineAfterDelete()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemLines/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }
    }
}