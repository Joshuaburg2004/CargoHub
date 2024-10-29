using System.Net;

namespace PythonTests
{
    [TestCaseOrderer("PythonTests.PriorityOrderer", "PythonTests")]
    public class ItemGroupTest : BaseTest
    {
        public ItemGroupTest() : base() { }

        [Fact, TestPriority(1)]
        public async Task GetAllItemGroups()
        {
            var requestUri = "/api/v1/item_groups";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetOneItemGroup()
        {
            var requestUri = "/api/v1/item_groups/1";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(3)]
        public async Task CreateItemGroup()
        {
            var requestUri = "/api/v1/item_groups";
            var response = await _client.PostAsync(requestUri, new StringContent("{\"id\": 100, \"name\": \"ItemGroup1\", \"description\": \"ItemGroup1 description\", \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\"}"));
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        // returns 404 not found, should return 201 created

        [Fact, TestPriority(4)]
        public async Task GetItemGroupAfterAdding()
        {
            var requestUri = "/api/v1/item_groups/100";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        // since the CreateItemGroup test fails, this test should also fail, but it returns 200 OK

        [Fact, TestPriority(5)]
        public async Task PutItemGroup()
        {
            var requestUri = "/api/v1/item_groups/100";
            var response = await _client.PutAsync(requestUri, new StringContent("{\"id\": 100, \"name\": \"ItemGroup1\", \"description\": \"ItemGroup1 description\", \"created_at\": \"1983-09-26T19:06:08Z\", \"updated_at\": \"1983-09-28T15:06:08Z\"}"));
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        // since the CreateItemGroup test fails, this test should also fail, but it returns 200 OK

        [Fact, TestPriority(6)]
        public async Task GetItemGroupAfterUpdating()
        {
            var requestUri = "/api/v1/item_groups/100";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        // since the CreateItemGroup test fails, this test should also fail, but it returns 200 OK

        [Fact, TestPriority(7)]
        public async Task DeleteItemGroup()
        {
            var requestUri = "/api/v1/item_groups/100";
            var response = await _client.DeleteAsync(requestUri);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        // since the CreateItemGroup test fails, this test should also fail, but it returns 200 OK

        [Fact, TestPriority(8)]
        public async Task GetItemGroupAfterDelete()
        {
            var requestUri = "/api/v1/item_groups/100";
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        // This test should not fail because the item group with id 100 should not exist, but it returns 200 OK
    }
}