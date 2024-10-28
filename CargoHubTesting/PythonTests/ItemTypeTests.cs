using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;
using PythonTests.models;

namespace PythonTests;

[TestCaseOrderer("MyTests.PriorityOrderer", "PythonTests")]

public class Item_typeIntegratieTest : BaseTest
{

    public static item_type testType = new(0, "Laptop", "");
    public static item_type PutType = new(0, "smart name", "smart description");
    public static string testTypeJson {get => JsonSerializer.Serialize(testType);}
    public static Item TestItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 0, 0,0,0,0,0,0,"SUP423", "E-86805-uTM");
    public Item_typeIntegratieTest(): base()
    {}

    [Fact, TestPriority(0)]
    public async Task GetAllItemtypesOne()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/v1/item_types");
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<item_type>? returnedlist = JsonSerializer.Deserialize<List<item_type>>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<List<item_type>>(returnedlist);
        Xunit.Assert.Single(returnedlist);
        
        item_type returned = returnedlist[0];
        Xunit.Assert.Equal(testType.id, returned.id);
        Xunit.Assert.Equal(testType.name, returned.name);
        Xunit.Assert.Equal(testType.description, returned.description);
    }

    [Fact, TestPriority(1)]
    public async Task GetItemtype()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_types/0");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        item_type? returned = JsonSerializer.Deserialize<item_type>(await response.Content.ReadAsStringAsync());
        Xunit.Assert.IsType<item_type>(returned);
        Xunit.Assert.Equal(testType.id, returned.id);
        Xunit.Assert.Equal(testType.name, returned.name);
        Xunit.Assert.Equal(testType.description, returned.description);
    }

    [Fact, TestPriority(2)]
    public async Task UpdateItemtype()
    {
        string toSend = JsonSerializer.Serialize(PutType);
        HttpResponseMessage response = await _client.PutAsync($"/api/v1/item_types/0", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
        
    }

    [Fact, TestPriority(3)]
    public async Task GetUpdatedItemType()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_types/0");
        var responseContent = await response.Content.ReadAsStringAsync();
        item_type? itemtypeafterupdate = JsonSerializer.Deserialize<item_type>(responseContent);

        Xunit.Assert.IsType<item_type>(itemtypeafterupdate);

        Xunit.Assert.Equal(PutType.id, itemtypeafterupdate.id);
        Xunit.Assert.Equal(PutType.name, itemtypeafterupdate.name);
        Xunit.Assert.Equal(PutType.description, itemtypeafterupdate.description);
    }

    [Fact, TestPriority(4)]
    public async Task GetItemTypeItems()
    {
        string toSend = JsonSerializer.Serialize(TestItem);
        HttpResponseMessage postresponse = await _client.PostAsync("/api/v1/items", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.Created, postresponse.StatusCode);
        

        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_types/0/items");
        var responseContent = await response.Content.ReadAsStringAsync();
        List<Item>? itemtypeafterupdate = JsonSerializer.Deserialize<List<Item>>(responseContent);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Xunit.Assert.IsType<List<Item>>(itemtypeafterupdate);
        
        Item ToReturn = itemtypeafterupdate[0];
        Xunit.Assert.Equal(TestItem.uid, ToReturn.uid);
        Xunit.Assert.Equal(TestItem.code, ToReturn.code);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/items/P000004");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(5)]
    public async Task GetWrongItemtype()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_types/2");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("null", responseContent);
    }

    [Fact, TestPriority(6)]
    public async Task DeleteItemtype()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/item_types/0");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("", responseContent);
    }

    [Fact, TestPriority(7)]
    public async Task GetItemTypeEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync($"/api/v1/item_types");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
