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
    public async Task<IActionResult> AddClient([FromBody] Client client) => Ok(await Clients.AddClient(client));
    [HttpPut()]
    public async Task<IActionResult> UpdateClient([FromQuery] Guid guid, [FromBody] Client client) => Ok(await Clients.UpdateClient(guid, client));
    [HttpDelete()]
    public async Task<IActionResult> DeleteClient([FromQuery] Guid guid) => Ok(await Clients.RemoveClient(guid));
    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetOrders(Guid ordersId) => throw new NotImplementedException();
}
