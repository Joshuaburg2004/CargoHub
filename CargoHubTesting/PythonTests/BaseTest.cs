using Xunit;
public class BaseTest
{
    protected readonly HttpClient _client;

    public BaseTest()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:3000");
        _client.DefaultRequestHeaders.Add("API_KEY", "a1b2c3d4e5");
    }
}