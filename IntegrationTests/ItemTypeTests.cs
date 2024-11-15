using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Xunit.Abstractions;
using CargoHubAlt.Models;
using System.Net.Http.Json;

namespace IntegrationTests;

[TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]

public class Item_typeIntegratieTest : BaseTest
{

    public static ItemType testType = new(1, "Laptop", "");
    public static ItemType PutType = new(1, "smart name", "smart description");
    public static Item TestItem = new("P000004", "sjQ23408K", "Face-to-face clear-thinking complexity",
     "must", "6523540947122", "63-OFFTq0T", "oTo304", 1, 1,1,1,1,1,1,"SUP423", "E-86805-uTM");

    private static string _requestUri= "/api/v1/item_types";

    public Item_typeIntegratieTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {}

    
    [Fact, TestPriority(0)]
    public async Task GetItemTypeEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync(_requestUri);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    } 
    
    [Fact, TestPriority(1)]
    public async Task PostItemType()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(_requestUri, testType);
        Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(2)]
    public async Task GetAllItemtypesOne()
    {
        HttpResponseMessage response = await _client.GetAsync(_requestUri);
        Xunit.Assert.NotNull(response);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<ItemType>? returnedlist = await response.Content.ReadFromJsonAsync<List<ItemType>>();
        Xunit.Assert.IsType<List<ItemType>>(returnedlist);
        Xunit.Assert.Single(returnedlist);
        
        ItemType returned = returnedlist[0];
        Xunit.Assert.Equal(testType.Id, returned.Id);
        Xunit.Assert.Equal(testType.Name, returned.Name);
        Xunit.Assert.Equal(testType.Description, returned.Description);
    }

    [Fact, TestPriority(3)]
    public async Task GetItemtype()
    {
        HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/1");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ItemType? returned = await response.Content.ReadFromJsonAsync<ItemType>();
        Xunit.Assert.IsType<ItemType>(returned);
        Xunit.Assert.Equal(testType.Id, returned.Id);
        Xunit.Assert.Equal(testType.Name, returned.Name);
        Xunit.Assert.Equal(testType.Description, returned.Description);
    }

    [Fact, TestPriority(4)]
    public async Task UpdateItemtype()
    {
        string toSend = JsonSerializer.Serialize(PutType);
        HttpResponseMessage response = await _client.PutAsync($"{_requestUri}/1", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("", await response.Content.ReadAsStringAsync());
        
    }

    [Fact, TestPriority(5)]
    public async Task GetUpdatedItemType()
    {
        HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/1");
        var responseContent = await response.Content.ReadAsStringAsync();
        ItemType? itemtypeafterupdate = await response.Content.ReadFromJsonAsync<ItemType>();

        Xunit.Assert.IsType<ItemType>(itemtypeafterupdate);

        Xunit.Assert.Equal(PutType.Id, itemtypeafterupdate.Id);
        Xunit.Assert.Equal(PutType.Name, itemtypeafterupdate.Name);
        Xunit.Assert.Equal(PutType.Description, itemtypeafterupdate.Description);
    }

    [Fact, TestPriority(6)]
    public async Task GetItemTypeItems()
    {
        string toSend = JsonSerializer.Serialize(TestItem);
        HttpResponseMessage postresponse = await _client.PostAsync("/api/v1/items", new StringContent(toSend, System.Text.Encoding.UTF8, "application/json"));
        Xunit.Assert.Equal(HttpStatusCode.Created, postresponse.StatusCode);
        

        HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/1/items");
        var responseContent = await response.Content.ReadAsStringAsync();
        List<IntegrationTests.models.Item>? itemtypeafterupdate = JsonSerializer.Deserialize<List<IntegrationTests.models.Item>>(responseContent);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Xunit.Assert.IsType<List<IntegrationTests.models.Item>>(itemtypeafterupdate);
        
        IntegrationTests.models.Item ToReturn = itemtypeafterupdate[0];
        Xunit.Assert.Equal(TestItem.Uid, ToReturn.uid);
        Xunit.Assert.Equal(TestItem.Code, ToReturn.code);

        HttpResponseMessage responsedelete = await _client.DeleteAsync("/api/v1/items/P000004");

        Xunit.Assert.Equal(HttpStatusCode.OK, responsedelete.StatusCode);
    }

    [Fact, TestPriority(7)]
    public async Task GetWrongItemtype()
    {
        HttpResponseMessage response = await _client.GetAsync($"{_requestUri}/2");
        Console.Error.WriteLine(await response.Content.ReadAsStringAsync());
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("null", responseContent);
    }

    [Fact, TestPriority(8)]
    public async Task DeleteItemtype()
    {
        HttpResponseMessage response = await _client.DeleteAsync($"{_requestUri}/1");
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("", responseContent);
    }

    [Fact, TestPriority(9)]
    public async Task GetItemTypeAfterDelete()
    {
        HttpResponseMessage response = await _client.GetAsync(_requestUri);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Xunit.Assert.Equal("[]", responseContent);
    }
}
