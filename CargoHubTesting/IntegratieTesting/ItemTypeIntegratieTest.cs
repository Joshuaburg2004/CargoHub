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

    public class Item_typeIntegratieTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;
        public static Guid itemtype1Id;
        public static Item_type? itemtypeafterupdate;

        public Item_typeIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllItemtypesEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemTypes/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }

        [Fact, TestPriority(1)]
        public async Task CreateItemtype()
        {
            HttpResponseMessage response = await _client.PostAsync("/api/v1/itemtypes", new StringContent("{\"id\":\"1F3B29DE-6F73-4A31-BF7B-F9E5C3F8C92C\",\"Name\":\"Item Type Name\",\"Description\":\"Item Type Description\",\"created_at\":\"2021-06-01T00:00:00\",\"updated_at\":\"2021-06-01T00:00:00\"}", System.Text.Encoding.UTF8, "application/json"));
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Guid? itemtypeid = string.IsNullOrEmpty(responseContent) ? null :  JsonSerializer.Deserialize<Guid>(responseContent, options);
            Xunit.Assert.NotNull(itemtypeid);
            Guid guid = itemtypeid ?? Guid.Empty;
            itemtype1Id = guid;

            Xunit.Assert.NotEqual(Guid.Empty, itemtypeid);
        }

        [Fact, TestPriority(2)]
        public async Task GetAllItemtypes()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemTypes/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotEqual("[]", responseContent);
        }

        [Fact, TestPriority(3)]
        public async Task GetItemtype()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/itemTypes/getbyid?id={itemtype1Id}");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Item Type Name", responseContent);
            Xunit.Assert.Contains("Item Type Description", responseContent);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateItemtype()
        {
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/itemTypes?id={itemtype1Id}", new StringContent("{\"id\":\"" + itemtype1Id + "\",\"Name\":\"Item Type Name\",\"Description\":\"Item Type Description\",\"items\":[{\"amount\": 5,\"itemTypeId\": 0}],\"created_at\":\"2021-06-01T00:00:00\",\"UpdatedAt\":\"2021-06-01T00:00:00\"}", System.Text.Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            itemtypeafterupdate = string.IsNullOrEmpty(responseContent) ? null : JsonSerializer.Deserialize<Item_type>(responseContent, options);

            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.NotNull(itemtypeafterupdate);
            Xunit.Assert.Equal(itemtype1Id, itemtypeafterupdate.Id);
            Xunit.Assert.Equal("Item Type Name", itemtypeafterupdate.Name);
        }

        [Fact, TestPriority(5)]
        public async Task GetWrongItemtype()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/itemTypes/getbyid?id=00000000-0000-0000-0000-000000000000");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("ID", responseContent);
            Xunit.Assert.Contains("is empty", responseContent);
        }

        [Fact, TestPriority(6)]
        public async Task DeleteItemtype()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/itemTypes?id={itemtype1Id}");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Contains("Item Type Name", responseContent);
            Xunit.Assert.Contains("Item Type Description", responseContent);
        }
    }
}