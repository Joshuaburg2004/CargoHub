using System.Net;
using System.Net.Http;
using IntegrationTests;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using CargoHubAlt.Models;
using System.Net.Http.Json;
namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class SupplierTest : BaseTest
    {
        public string requestUri = "/api/v2/suppliers";
        private Supplier _newsupplier = new Supplier(1, "SUP0001", "Lee, Parks and Johnson", "5989 Sullivan Drives", "Apt. 996", "Port Anitaburgh", "91688", "Illinois", "Czech Republic", "Toni Barnett", "363.541.7282x36825", "LPaJ-SUP0001");
        private Supplier _supplierToPut = new Supplier(1, "SUP0001", "Bob, Parks and Johnson", "5989 Sullivan Drives", "Apt. 996", "Port Anitaburgh", "91688", "Illinois", "Czech Republic", "Toni Barnett", "363.541.7282x36825", "LPaJ-SUP0001");
        private Item _item = new Item("P000001", "ITM0001", "Item 1", "Item 1", "UPC0001", "Model 1", "Commodity 1", 1, 1, 1, 1, 1, 1, 1, "SUP0001", "SUP0001-ITM0001");
        public SupplierTest(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        [Fact, TestPriority(1)]
        public async Task GetAllSuppliers()
        {
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }
        [Fact, TestPriority(2)]
        public async Task GetOneSupplierBeforeAdding()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
            // It should absolutely be either 400 bad request or 404 not found, but it is 200 OK.
            // This is not intended behavior.
        }
        [Fact, TestPriority(3)]
        public async Task CreateSupplier()
        {
            var response = await _client.PostAsJsonAsync(requestUri, _newsupplier);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact, TestPriority(4)]
        public async Task GetOneSupplierAfterAdding()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Supplier? supplier = await response.Content.ReadFromJsonAsync<Supplier>();

            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(_newsupplier.Id, supplier.Id);
            Xunit.Assert.Equal(_newsupplier.Code, supplier.Code);
            Xunit.Assert.Equal(_newsupplier.Name, supplier.Name);
            Xunit.Assert.Equal(_newsupplier.Address, supplier.Address);
            Xunit.Assert.Equal(_newsupplier.AddressExtra, supplier.AddressExtra);
            Xunit.Assert.Equal(_newsupplier.City, supplier.City);
            Xunit.Assert.Equal(_newsupplier.ZipCode, supplier.ZipCode);
            Xunit.Assert.Equal(_newsupplier.Province, supplier.Province);
            Xunit.Assert.Equal(_newsupplier.Country, supplier.Country);
            Xunit.Assert.Equal(_newsupplier.ContactName, supplier.ContactName);
            Xunit.Assert.Equal(_newsupplier.Phonenumber, supplier.Phonenumber);
            Xunit.Assert.Equal(_newsupplier.Reference, supplier.Reference);
        }

        [Fact, TestPriority(5)]
        public async Task GetItemsSupplier()
        {
            var itemrequestUri = "/api/v2/items";
            var itemresponse = await _client.PostAsJsonAsync(itemrequestUri, _item);
            var itemresult = await itemresponse.Content.ReadAsStringAsync();

            var response = await _client.GetAsync($"{requestUri}/1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            List<Item>? items = await response.Content.ReadFromJsonAsync<List<Item>>();
            Xunit.Assert.NotNull(items);
            Xunit.Assert.Equal(_item.Uid, items[0].Uid);
            Xunit.Assert.Equal(_item.Code, items[0].Code);
            Xunit.Assert.Equal(_item.Description, items[0].Description);
            Xunit.Assert.Equal(_item.ShortDescription, items[0].ShortDescription);
            Xunit.Assert.Equal(_item.UpcCode, items[0].UpcCode);
            Xunit.Assert.Equal(_item.ModelNumber, items[0].ModelNumber);
            Xunit.Assert.Equal(_item.CommodityCode, items[0].CommodityCode);
            Xunit.Assert.Equal(_item.ItemLine, items[0].ItemLine);
            Xunit.Assert.Equal(_item.ItemGroup, items[0].ItemGroup);
            Xunit.Assert.Equal(_item.ItemType, items[0].ItemType);
            Xunit.Assert.Equal(_item.UnitPurchaseQuantity, items[0].UnitPurchaseQuantity);
            Xunit.Assert.Equal(_item.UnitOrderQuantity, items[0].UnitOrderQuantity);
            Xunit.Assert.Equal(_item.PackOrderQuantity, items[0].PackOrderQuantity);
            Xunit.Assert.Equal(_item.SupplierId, items[0].SupplierId);
            Xunit.Assert.Equal(_item.SupplierCode, items[0].SupplierCode);
            Xunit.Assert.Equal(_item.SupplierPartNumber, items[0].SupplierPartNumber);
        }

        [Fact, TestPriority(5)]
        public async Task PutSupplier()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1", _supplierToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(6)]
        public async Task GetOneSupplierAfterPutting()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Supplier? supplier = await response.Content.ReadFromJsonAsync<Supplier>();

            Xunit.Assert.NotNull(supplier);
            Xunit.Assert.Equal(_supplierToPut.Id, supplier.Id);
            Xunit.Assert.Equal(_supplierToPut.Code, supplier.Code);
            Xunit.Assert.Equal(_supplierToPut.Name, supplier.Name);
            Xunit.Assert.Equal(_supplierToPut.Address, supplier.Address);
            Xunit.Assert.Equal(_supplierToPut.AddressExtra, supplier.AddressExtra);
            Xunit.Assert.Equal(_supplierToPut.City, supplier.City);
            Xunit.Assert.Equal(_supplierToPut.ZipCode, supplier.ZipCode);
            Xunit.Assert.Equal(_supplierToPut.Province, supplier.Province);
            Xunit.Assert.Equal(_supplierToPut.Country, supplier.Country);
            Xunit.Assert.Equal(_supplierToPut.ContactName, supplier.ContactName);
            Xunit.Assert.Equal(_supplierToPut.Phonenumber, supplier.Phonenumber);
            Xunit.Assert.Equal(_supplierToPut.Reference, supplier.Reference);
        }
        [Fact, TestPriority(7)]
        public async Task DeleteSupplier()
        {
            var response = await _client.DeleteAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact, TestPriority(8)]
        public async Task GetOneSupplierAfterDelete()
        {
            var response = await _client.GetAsync($"{requestUri}/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest) || response.StatusCode.Equals(HttpStatusCode.NotFound));
        }
        [Fact, TestPriority(9)]
        public async Task GetAllShipmentsAfterDelete()
        {
            var response = await _client.GetAsync(requestUri);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Xunit.Assert.Equal("[]", result);
        }

        [Fact, TestPriority(10)]
        public async Task GetItemsSupplierNotFound()
        {
            var itemrequestUri = "/api/v1/items/P000001";
            var itemresponse = await _client.DeleteAsync(itemrequestUri);

            var response = await _client.GetAsync($"{requestUri}/1/items");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(11)]
        public async Task DeleteSupplierIdNegative()
        {
            var response = await _client.DeleteAsync("/api/v1/suppliers/-1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(12)]
        public async Task DeleteSupplierNotFound()
        {
            var response = await _client.DeleteAsync("/api/v1/suppliers/1");
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestPriority(13)]
        public async Task UpdateSupplierIdNegative()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/-1", _supplierToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(14)]
        public async Task UpdateSupplierNotFound()
        {
            var response = await _client.PutAsJsonAsync($"{requestUri}/1", _supplierToPut);
            var result = await response.Content.ReadAsStringAsync();
            Xunit.Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}