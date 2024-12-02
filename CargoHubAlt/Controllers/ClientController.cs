using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v1/clients")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        public IClientService Clients { get; set; }
        public ClientController(IClientService clients, ILogger<ClientController> logger)
        {
            Clients = clients;
            _logger = logger;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllClients()
        {
            List<Client>? clients = await Clients.GetAllClients();
            _logger.LogInformation($"Found {clients.Count} clients");
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneClient([FromRoute] int id)
        {
            if (id < 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest("ID cannot be smaller than 0.");
            }
            var client = await Clients.GetClient(id);
            if (client == null)
            {
                _logger.LogInformation($"Client with id:{id} not found");
                return BadRequest("The requested id was not found.");
            }
            _logger.LogInformation($"Client with id:{id} found");
            return Ok(client);
        }
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersByClient([FromRoute] int id)
        {
            if (id < 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest("ID cannot be smaller than 0.");
            }
            List<Order> orders = await Clients.GetOrdersByClient(id);
            if (orders.Count == 0)
            {
                _logger.LogInformation($"No orders found for client with id:{id}");
                return BadRequest("The requested id was not found as the bill to or ship to destination.");
            }
            _logger.LogInformation($"Found {orders.Count} orders for client with id:{id}");
            return Ok(orders);
        }
        [HttpPost()]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            if (client == null)
            {
                _logger.LogInformation("Client cannot be null");
                return BadRequest("Client cannot be null.");
            }
            await Clients.AddClient(client);
            _logger.LogInformation($"Created new client with id:{client.Id}");
            return Created("Created client:", client);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] Client client)
        {
            if (id < 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest("ID cannot be smaller than 0.");
            }
            if (client == null)
            {
                _logger.LogInformation("Client cannot be null");
                return BadRequest("Client cannot be null.");
            }
            await Clients.UpdateClient(id, client);
            _logger.LogInformation($"Updated client with id:{id}");
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (id < 0)
            {
                _logger.LogInformation("Invalid id");
                return BadRequest("ID cannot be smaller than 0.");
            }
            await Clients.RemoveClient(id);
            _logger.LogInformation($"Deleted client with id:{id}");
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
