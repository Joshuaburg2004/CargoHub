using Microsoft.AspNetCore.Mvc;

[Route("api/v2/client")]
public class ClientV2Controller : Controller{
    public IClients Clients{ get; set; }
    public ClientV2Controller(IClients clients){
        Clients = clients;
    }
    [HttpGet()]
    public async Task<IActionResult> GetAllClients() => Ok(await Clients.GetAllClients());
    [HttpGet()]
    public async Task<IActionResult> GetBatchClients([FromQuery] Guid[] guids) => Ok(await Clients.GetBatchClients(guids));
    
    [HttpGet()]
    public async Task<IActionResult> GetOneClient([FromQuery] Guid clientId) => Ok(await Clients.GetClient(clientId));
}
