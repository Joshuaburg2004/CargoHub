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

    public class Item_groupIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid itemgroup1Id;
        public static Item_group? itemgroupafterupdate;

        public Item_groupIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllItemgroupsEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemGroups/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateItemgroup()
        {
            HttpResponseMessage response = await _client.PostAsync("/api/v1/itemGroups", new StringContent("{\"Name\":\"Item Group Name\",\"Description\":\"Item Group Description\"}", System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Guid? itemgroupid = string.IsNullOrEmpty(responseContent) ? null :  JsonSerializer.Deserialize<Guid>(responseContent, options);
            Xunit.Assert.NotNull(itemgroupid);
            Guid guid = itemgroupid ?? Guid.Empty;
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            itemgroup1Id = guid;

            Xunit.Assert.NotEqual(Guid.Empty, itemgroupid);
        }

        [Fact, TestPriority(2)]
        public async Task GetAllItemgroups()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemGroups/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        [Fact, TestPriority(3)]
        public async Task GetItemgroup()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/itemGroups/getbyid?id={itemgroup1Id}");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Item Group Name", responseContent);
            Xunit.Assert.Contains("Item Group Description", responseContent);
        }

        [Fact, TestPriority(4)]
        public async Task DeleteItemgroup()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/itemGroups?id={itemgroup1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Item Group Name", responseContent);
            Xunit.Assert.Contains("Item Group Description", responseContent);
        }
    }
}