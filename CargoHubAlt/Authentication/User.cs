public class User{
    public string ApiKey;
    public string App;
    public EndpointAccesses EndpointAccess;
    public User(string ApiKey, string App, EndpointAccesses endpointAccess)
    {
        this.ApiKey = ApiKey;
        this.App = App;
        this.EndpointAccess = endpointAccess;
    }
}