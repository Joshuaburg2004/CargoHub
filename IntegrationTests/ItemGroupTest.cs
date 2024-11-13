using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit;
using CargoHubAlt.Models;
namespace IntegrationTests{

    [TestCaseOrderer("IntegrationTests.PriorityOrderer","IntegrationTests")]

    public class ItemGroupTest : BaseTest
    {

        public static ItemGroup testType = new(1, "Laptop", "");
        public static ItemGroup PutType = new(1, "smart Name", "smart Description");
        public static string testTypeJson {get => JsonSerializer.Serialize(testType);}
        public static Item TestItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
        "must", "6523540947122", "63-OFFTq0T", "oTo304", 1, 1,1,1,1,1,1,"SUP423", "E-86805-uTM");
        public ItemGroupTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {}

        [Fact, TestPriority(0)]
        public async Task GetAllItemGroupsOne()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/item_groups");
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<ItemGroup>? returnedlist = JsonSerializer.Deserialize<List<ItemGroup>>(await response.Content.ReadAsStringAsync());
            Xunit.Assert.IsType<List<ItemGroup>>(returnedlist);
            Xunit.Assert.Single(returnedlist);
            
            ItemGroup returned = returnedlist[0];
            Xunit.Assert.Equal(testType.Id, returned.Id);
            Xunit.Assert.Equal(testType.Name, returned.Name);
            Xunit.Assert.Equal(testType.Description, returned.Description);
        }

        [Fact, TestPriority(1)]
        public async Task GetItemGroup()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/1");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            ItemGroup? returned = JsonSerializer.Deserialize<ItemGroup>(await response.Content.ReadAsStringAsync());
            Xunit.Assert.IsType<ItemGroup>(returned);
            Xunit.Assert.Equal(testType.Id, returned.Id);
            Xunit.Assert.Equal(testType.Name, returned.Name);
            Xunit.Assert.Equal(testType.Description, returned.Description);
        }

        [Fact, TestPriority(2)]
        public async Task UpdateItemGroup()
        {
            string toSend = JsonSerializer.Serialize(PutType);
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/item_groups/1", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
            
        }

        [Fact, TestPriority(3)]
        public async Task GetUpdatedItemGroup()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/1");
            var responseContent = await response.Content.ReadAsStringAsync();
            ItemGroup? ItemGroupafterupdate = JsonSerializer.Deserialize<ItemGroup>(responseContent);

            Xunit.Assert.IsType<ItemGroup>(ItemGroupafterupdate);

            Xunit.Assert.Equal(PutType.Id, ItemGroupafterupdate.Id);
            Xunit.Assert.Equal(PutType.Name, ItemGroupafterupdate.Name);
            Xunit.Assert.Equal(PutType.Description, ItemGroupafterupdate.Description);
        }

        [Fact, TestPriority(4)]
        public async Task GetItemGroupItems()
        {
            string toSend = JsonSerializer.Serialize(TestItem);
            HttpResponseMessage postresponse = await _client.PostAsync("/api/v1/items", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.Created, postresponse.StatusCode);
            

            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/1/items");
            var responseContent = await response.Content.ReadAsStringAsync();
            List<Item>? ItemGroupafterupdate = JsonSerializer.Deserialize<List<Item>>(responseContent);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Xunit.Assert.IsType<List<Item>>(ItemGroupafterupdate);
            
            Item ToReturn = ItemGroupafterupdate[0];
            Xunit.Assert.Equal(TestItem.Uid, ToReturn.Uid);
            Xunit.Assert.Equal(TestItem.Code, ToReturn.Code);

            HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/items/P000004");

            Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
        }

        [Fact, TestPriority(5)]
        public async Task GetWrongItemGroup()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/2");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("null", responseContent);
        }

        [Fact, TestPriority(6)]
        public async Task DeleteItemGroup()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/item_groups/1");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("", responseContent);
        }

        [Fact, TestPriority(7)]
        public async Task GetItemGroupEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal("[]", responseContent);
        }
    }
}