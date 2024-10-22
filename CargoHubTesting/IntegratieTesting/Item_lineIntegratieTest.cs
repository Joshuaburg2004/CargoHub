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
        public static Guid item_line1Id;
        public static Client item_lineafterupdate;

        public Item_lineIntegratieTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, TestPriority(0)]
        public async Task GetAllItem_linesEmpty()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/itemLines/getall");
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }
    }
}