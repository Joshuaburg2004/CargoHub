using Microsoft.AspNetCore.Mvc;

[Route("api/v1/client")]
public class ClientV1Controller : Controller
{
    public IClientService Clients { get; set; }
    public ClientV1Controller(IClientService clients)
    {
        Clients = clients;
    }
    [HttpGet("getall")]
    public async Task<IActionResult> GetAllClients() => Ok(await Clients.GetAllClients());
    [HttpGet()]
    public async Task<IActionResult> GetOneClient([FromQuery] Guid id) => Ok(await Clients.GetClient(id));
    [HttpPost()]
    public async Task<IActionResult> AddClient([FromBody] Client client) => Created("Client created!", await Clients.AddClient(client));
    [HttpPut()]
    public async Task<IActionResult> UpdateClient([FromQuery] Guid guid, [FromBody] Client client) => Ok(await Clients.UpdateClient(guid, client));
    [HttpDelete()]
    public async Task<IActionResult> DeleteClient([FromQuery] Guid guid) => Ok(await Clients.RemoveClient(guid));
    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetOrders(Guid ordersId) => throw new NotImplementedException();

}
