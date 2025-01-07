using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers.ControllersV2
{
    [ApiController]
    [Route("api/v2/clients")]
    public class ClientControllerV2 : Controller
    {
        private readonly ILogger<ClientControllerV2> _logger;
        public IClientServiceV2 Clients { get; set; }
        public ClientControllerV2(IClientServiceV2 clients, ILogger<ClientControllerV2> logger)
        {
            Clients = clients;
            _logger = logger;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllClients([FromQuery] int? pageIndex)
        {

            List<Client> clients = await Clients.GetAllClients(pageIndex);
            _logger.LogInformation($"Found {clients.Count} clients");
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneClient([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            var client = await Clients.GetClient(id);
            if (client == null)
            {
                return BadRequest("The requested id was not found.");
            }
            return Ok(client);
        }
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersByClient([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            List<Order> orders = await Clients.GetOrdersByClient(id);
            if (orders.Count == 0)
            {
                return BadRequest("The requested id was not found as the bill to or ship to destination.");
            }
            return Ok(orders);
        }
        [HttpPost()]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            var apiKey = Request.Headers["api_key"];
            if (client == null)
            {
                return BadRequest("Client cannot be null.");
            }
            await Clients.AddClient(client);
            _logger.LogInformation($"Created new client with id:{client.Id}, ApiKey:{apiKey}");
            return Created("Created client:", client);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] Client client)
        {
            var apiKey = Request.Headers["api_key"];
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            if (client == null)
            {
                return BadRequest("Client cannot be null.");
            }
            await Clients.UpdateClient(id, client);
            _logger.LogInformation($"Updated client with id:{id}, ApiKey:{apiKey}");
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            var apiKey = Request.Headers["api_key"];
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            await Clients.RemoveClient(id);
            _logger.LogInformation($"Deleted client with id:{id}, ApiKey:{apiKey}");
            return Ok();
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await Clients.LoadFromJson(path);
            return Ok();
        }
    }
}
