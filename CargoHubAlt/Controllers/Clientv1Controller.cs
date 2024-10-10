using Microsoft.AspNetCore.Mvc;

[Route("api/v1/client")]
public class ClientV1Controller : Controller{
    public IClients Clients{ get; set; }
    public ClientV1Controller(IClients clients){
        Clients = clients;
    }
    [HttpGet()]
    public async Task<IActionResult> GetAllClients() => Ok(await Clients.GetAllClients());
    [HttpGet()]
    public async Task<IActionResult> GetOneClient([FromQuery] Guid clientId) => Ok(await Clients.GetClient(clientId));
    [HttpPost()]
    public async Task<IActionResult> AddClient([FromBody] Client client) => Ok(await Clients.AddClient(client));
    
}
