using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit;
using IntegrationTests.models;

namespace IntegrationTests;

[TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]

public class ItemLineTest : BaseTest
{

    public static item_line testType = new(1, "Laptop", "");
    public static item_line PutType = new(1, "smart name", "smart description");
    public static string testTypeJson {get => JsonSerializer.Serialize(testType);}
    public static IntegrationTests.models.Item TestItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 1, 1,1,1,1,1,1,"SUP423", "E-86805-uTM");
    public ItemLineTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {}

    [Fact, TestPriority(0)]
    public async Task GetAllItemLinesOne()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/item_lines");
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<item_line>? returnedlist = JsonSerializer.Deserialize<List<item_line>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<item_line>>(returnedlist);
        Xunit.Assert.Single(returnedlist);
        
        item_line returned = returnedlist[0];
        Xunit.Assert.Equal(testType.id, returned.id);
        Xunit.Assert.Equal(testType.name, returned.name);
        Xunit.Assert.Equal(testType.description, returned.description);
    }

    [Fact, TestPriority(1)]
    public async Task GetItemLine()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_lines/1");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        item_line? returned = JsonSerializer.Deserialize<item_line>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<item_line>(returned);
        Xunit.Assert.Equal(testType.id, returned.id);
        Xunit.Assert.Equal(testType.name, returned.name);
        Xunit.Assert.Equal(testType.description, returned.description);
    }

    [Fact, TestPriority(2)]
    public async Task UpdateItemLine()
    {
        string toSend = JsonSerializer.Serialize(PutType);
        HttpResponseMessage response = await _client.PutAsync($"/api/v1/item_lines/1", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
        
    }

    [Fact, TestPriority(3)]
    public async Task GetUpdatedItemLine()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_lines/1");
        var responseContent = await response.Content.ReadAsStringAsync();
        item_line? ItemLineafterupdate = JsonSerializer.Deserialize<item_line>(responseContent);

        Xunit.Assert.IsType<item_line>(ItemLineafterupdate);

        Xunit.Assert.Equal(PutType.id, ItemLineafterupdate.id);
        Xunit.Assert.Equal(PutType.name, ItemLineafterupdate.name);
        Xunit.Assert.Equal(PutType.description, ItemLineafterupdate.description);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemLineItems()
    {
        string toSend = JsonSerializer.Serialize(TestItem);
        HttpResponseMessage postresponse = await _client.PostAsync("/api/v1/items", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.Created, postresponse.StatusCode);
        

        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_lines/1/items");
        var responseContent = await response.Content.ReadAsStringAsync();
        List<IntegrationTests.models.Item>? ItemLineafterupdate = JsonSerializer.Deserialize<List<IntegrationTests.models.Item>>(responseContent);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Xunit.Assert.IsType<List<IntegrationTests.models.Item>>(ItemLineafterupdate);
        
        IntegrationTests.models.Item ToReturn = ItemLineafterupdate[0];
        Xunit.Assert.Equal(TestItem.uid, ToReturn.uid);
        Xunit.Assert.Equal(TestItem.code, ToReturn.code);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/items/P000004");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task GetWrongItemLine()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_lines/2");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("null", responseContent);
    }

    [Fact, TestPriority(6)]
    public async Task DeleteItemLine()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/item_lines/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("", responseContent);
    }

    [Fact, TestPriority(7)]
    public async Task GetItemLineEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_lines");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
