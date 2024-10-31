using Microsoft.AspNetCore.Mvc;

[Route("api/v1")]
public class ClientController : Controller
{
    public IClientService Clients { get; set; }
    public ClientController(IClientService clients)
    {
        Clients = clients;
    }
    

}
