using Microsoft.AspNetCore.Mvc;
using CargoHubAlt.Interfaces;
using CargoHubAlt.Models;
using Microsoft.AspNetCore.Http.Features;

namespace CargoHub.Controllers
{
    [ApiController]
    [Route("api/v1/suppliers")]
    public class SupplierController : Controller
    {
        private ISupplierService Suppliers { get; set; }
        public SupplierController(ISupplierService suppliers)
        {
            Suppliers = suppliers;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await Suppliers.GetAllSuppliers();
            return Ok(suppliers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await Suppliers.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound($"supplier with ID {id} not found");
            }
            return Ok(supplier);
        }
        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsForSupplier(int id)
        {
            List<Item>? items = await Suppliers.GetItemsfromSupplierById(id);
            if (items == null || items.Count == 0)
            {
                return NotFound($"no items found for supplier with ID {id}");
            }
            return Ok(items);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            var newSupplier = await Suppliers.AddSupplier(supplier);
            if (newSupplier == null)
            {
                return BadRequest("failed to create supplier");
            }
            return Created("Created supplier", newSupplier);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (id <= 0) return BadRequest("ID must be greater than 0");
            var supplier = await Suppliers.DeleteSupplier(id);
            if (supplier == null)
            {
                return NotFound($"supplier with ID {id} not found");
            }
            return Ok(supplier);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (id <= 0) return BadRequest("ID must be greater than 0");
            var oldSupplier = await Suppliers.UpdateSupplier(id, supplier);
            if (oldSupplier == null)
            {
                return NotFound($"supplier with ID {id} not found");
            }
            return Ok(oldSupplier);
        }
        [HttpPost("load/{path}")]
        public async Task<IActionResult> LoadClient([FromRoute] string path)
        {
            await Suppliers.LoadFromJson(path);
            return Ok();
        }
    }
}
