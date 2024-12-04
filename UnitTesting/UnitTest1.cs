using Xunit;
using CargoHubAlt.Services;
using CargoHubAlt.Models;
using CargoHubAlt.Interfaces;
using Moq;

namespace CargoHubUnitTests;

public class NaamServiceUnitTest //verander hier de naam
{
    [Fact]
    public void Test1()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(p => p.GetClient(It.IsAny<int>())).Returns(Task.FromResult(new Client()));
        var sut = new Service(mock.Object);
    }
}