using Microsoft.AspNetCore.Mvc;

[Route("api/v1/client")]
public class ClientV1Controller : Controller
{
    public IClientService Clients { get; set; }
    public ClientV1Controller(IClientService clients)
    {
        Clients = clients;
    }
    

}
