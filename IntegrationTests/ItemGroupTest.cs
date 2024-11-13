using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit;
using PythonTests.models;

namespace PythonTests{

    [TestCaseOrderer("PythonTests.PriorityOrderer","PythonTests")]

    public class item_groupIntegratieTest : BaseTest
    {

        public static item_group testType = new(1, "Laptop", "");
        public static item_group PutType = new(1, "smart name", "smart description");
        public static string testTypeJson {get => JsonSerializer.Serialize(testType);}
        public static PythonTests.models.Item TestItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
        "must", "6523540947122", "63-OFFTq0T", "oTo304", 1, 1,1,1,1,1,1,"SUP423", "E-86805-uTM");
        public item_groupIntegratieTest(): base()
        {}

        [Fact, TestPriority(0)]
        public async Task GetAllItemGroupsOne()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/item_groups");
            Xunit.Assert.NotNull(response);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<item_group>? returnedlist = JsonSerializer.Deserialize<List<item_group>>(await response.Content.ReadAsStringAsync());
            Xunit.Assert.IsType<List<item_group>>(returnedlist);
            Xunit.Assert.Single(returnedlist);
            
            item_group returned = returnedlist[0];
            Xunit.Assert.Equal(testType.id, returned.id);
            Xunit.Assert.Equal(testType.name, returned.name);
            Xunit.Assert.Equal(testType.description, returned.description);
        }

        [Fact, TestPriority(1)]
        public async Task GetItemGroup()
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/1");
            Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            item_group? returned = JsonSerializer.Deserialize<item_group>(await response.Content.ReadAsStringAsync());
            Xunit.Assert.IsType<item_group>(returned);
            Xunit.Assert.Equal(testType.id, returned.id);
            Xunit.Assert.Equal(testType.name, returned.name);
            Xunit.Assert.Equal(testType.description, returned.description);
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
            item_group? ItemGroupafterupdate = JsonSerializer.Deserialize<item_group>(responseContent);

            Xunit.Assert.IsType<item_group>(ItemGroupafterupdate);

            Xunit.Assert.Equal(PutType.id, ItemGroupafterupdate.id);
            Xunit.Assert.Equal(PutType.name, ItemGroupafterupdate.name);
            Xunit.Assert.Equal(PutType.description, ItemGroupafterupdate.description);
        }

        [Fact, TestPriority(4)]
        public async Task GetItemGroupItems()
        {
            string toSend = JsonSerializer.Serialize(TestItem);
            HttpResponseMessage postresponse = await _client.PostAsync("/api/v1/items", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
            Xunit.Assert.Equal(HttpStatusCode.Created, postresponse.StatusCode);
            

            HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_groups/1/items");
            var responseContent = await response.Content.ReadAsStringAsync();
            List<PythonTests.models.Item>? ItemGroupafterupdate = JsonSerializer.Deserialize<List<PythonTests.models.Item>>(responseContent);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Xunit.Assert.IsType<List<PythonTests.models.Item>>(ItemGroupafterupdate);
            
            PythonTests.models.Item ToReturn = ItemGroupafterupdate[0];
            Xunit.Assert.Equal(TestItem.uid, ToReturn.uid);
            Xunit.Assert.Equal(TestItem.code, ToReturn.code);

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