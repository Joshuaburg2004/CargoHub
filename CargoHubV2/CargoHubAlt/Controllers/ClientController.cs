using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;

namespace CargoHubAlt.Controllers
{
    [ApiController]
    [Route("api/v2/clients")]
    public class ClientController : Controller
    {
        public IClientService Clients { get; set; }
        public ClientController(IClientService clients)
        {
            Clients = clients;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await Clients.GetAllClients());
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
            if (client == null)
            {
                return BadRequest("Client cannot be null.");
            }
            await Clients.AddClient(client);
            return Created("Created client:", client);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] Client client)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            if (client == null)
            {
                return BadRequest("Client cannot be null.");
            }
            await Clients.UpdateClient(id, client);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be smaller than 0.");
            }
            await Clients.RemoveClient(id);
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
