// using System.Net;
// using System.Text;
// using System.Text.Json;
// using System.Text.Unicode;
// using CargoHubAlt.Models;
// using Newtonsoft.Json;
// using Xunit;

// namespace MyTests;

// [TestCaseOrderer("MyTests.PriorityOrderer", "CargoHubTesting")]
// public class WarehouseIntratieTest : IClassFixture<CustomWebApplicationFactory>
// {
//     private readonly HttpClient _client;
//     private readonly CustomWebApplicationFactory _factory;
//     public static Guid WarehouseId;
//     public static Guid ContactId;
//     public WarehouseIntratieTest(CustomWebApplicationFactory factory)
//     {
//         _factory = factory;
//         _client = _factory.CreateClient();
//     }

//     [Fact, TestPriority(0)]
//     public async Task TestGetAllWarehousesEmpty()
//     {
//         HttpResponseMessage response = await _client.GetAsync("/api/v1/warehouses");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//         Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
//     }

//     [Fact, TestPriority(1)]
//     public async Task TestCreateWarehouse()
//     {
//         var warehouse = new Warehouse
//         {
//             Id = Guid.NewGuid(),
//             Code = "NEWCODE123",
//             Name = "New Cargo Hub",
//             Address = "123 New Street",
//             Zip = "12345",
//             City = "New City",
//             Province = "New Province",
//             Country = "NC",
//             Contact = new Contact
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "John Doe",
//                 Phone = "(123) 456-7890",
//                 Email = "johndoe@example.com",
//                 CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
//                 UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
//             }
//         };
//         var jsonContent = JsonConvert.SerializeObject(warehouse);
//         var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
//         var response = await _client.PostAsync("api/v1/warehouses", content);

//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//         WarehouseId = warehouse.Id;
//         ContactId = warehouse.Contact.Id;
//     }

//     [Fact, TestPriority(2)]
//     public async Task TestGetAllWarehouses()
//     {
//         HttpResponseMessage response = await _client.GetAsync("/api/v1/warehouses");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//         Xunit.Assert.NotEqual("[]", await response.Content.ReadAsStringAsync());
//     }

//     [Fact, TestPriority(3)]
//     public async Task TestGetWarehousesById()
//     {
//         HttpResponseMessage response = await _client.GetAsync($"/api/v1/warehouses/{WarehouseId}");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//         var content = await response.Content.ReadAsStringAsync();
//         var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
//         Warehouse? warehouse = System.Text.Json.JsonSerializer.Deserialize<Warehouse>(content, options);

//         Xunit.Assert.NotNull(warehouse);
//         Xunit.Assert.Equal(WarehouseId, warehouse.Id);
//     }

//     [Fact, TestPriority(4)]
//     public async Task TestUpdateWarehouse()
//     {
//         var warehouse = new Warehouse
//         {
//             Id = WarehouseId,
//             Code = "NEWCODE123",
//             Name = "New Cargo Hub updated",
//             Address = "123 New Street",
//             Zip = "12345",
//             City = "New City",
//             Province = "New Province",
//             Country = "NC",
//             Contact = new Contact
//             {
//                 Id = ContactId,
//                 Name = "John Doe",
//                 Phone = "(123) 456-7890",
//                 Email = "johndoe@example.com",
//                 CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
//                 UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
//             }
//         };
//         var jsonContent = JsonConvert.SerializeObject(warehouse);
//         var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
//         var response = await _client.PutAsync($"/api/v1/warehouses", content);

//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }

//     [Fact, TestPriority(5)]
//     public async Task TestGetWarehousesByIdAfterUpdate()
//     {
//         HttpResponseMessage response = await _client.GetAsync($"/api/v1/warehouses/{WarehouseId}");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);

//         var content = await response.Content.ReadAsStringAsync();
//         var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
//         var warehouse = System.Text.Json.JsonSerializer.Deserialize<Warehouse>(content, options);

//         Xunit.Assert.NotNull(warehouse);
//         Xunit.Assert.Equal(WarehouseId, warehouse.Id);
//         Xunit.Assert.Equal("New Cargo Hub updated", warehouse.Name);
//     }

//     [Fact, TestPriority(6)]
//     public async Task TestDeleteWarehouse()
//     {
//         HttpResponseMessage response = await _client.DeleteAsync($"/api/v1/warehouses/{WarehouseId}");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }

//     [Fact, TestPriority(7)]
//     public async Task TestGetAllWarehousesAfterDelete()
//     {
//         HttpResponseMessage response = await _client.GetAsync("/api/v1/warehouses");
//         Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//         Xunit.Assert.Equal("[]", await response.Content.ReadAsStringAsync());
//     }
// }
