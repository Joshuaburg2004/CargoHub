using Xunit;
using CargoHubAlt.Services;
using CargoHubAlt.Models;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Controllers;
using CargoHubAlt.Database;
using Moq;

namespace CargoHubUnitTests;

public class NaamServiceUnitTest //verander hier de naam
{
    public Mock<IClientService> mockClientService = new Mock<IClientService>();

    [Fact]
    public async void Test1()
    {
        var mockClient = new Client
        {
            Id = 1,
            Name = "testname",
            Address = "testaddress",
            City = "testcity",
            ZipCode = "testzipcode",
            Province = "testprovince",
            Country = "testcountry",
            ContactName = "testcontactname",
            ContactPhone = "testcontactphone",
            ContactEmail = "testcontactemail",
        };

        mockClientService
                .Setup(service => service.GetClient(It.IsAny<int>()))
                .ReturnsAsync(mockClient);

        var Client = new ClientService(mockClientService.Object);
        var result = await Client.GetClient(1);

        Assert.NotNull(result);
        Assert.Equal(mockClient.Id, result.Id);
    }
}