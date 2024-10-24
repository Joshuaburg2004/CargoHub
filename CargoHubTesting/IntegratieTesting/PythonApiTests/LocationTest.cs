using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyTests;
using Xunit;
[TestCaseOrderer("MyTests.PriorityOrderer", "PythonTesting")]
public class LocationTest : BaseTest {
    public LocationTest() : base(){}
    [Fact, TestPriority(0)]
    public async Task GetAllLocations()
    {
        var requestUri = "/api/v1/locations";
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Xunit.Assert.Equal("[]", result);
    }
}