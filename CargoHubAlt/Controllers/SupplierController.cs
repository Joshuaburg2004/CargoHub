using Microsoft.AspNetCore.Mvc;
[Route("api/v1/suppliers")]
public class SupplierController : Controller
{
    private ISuppliers Suppliers { get; set; }
    public SupplierController(ISuppliers suppliers)
    {
        Suppliers = suppliers;
    }
    [HttpGet()]
    public async Task<IActionResult> GetSuppliers()
    {
        var suppliers = await Suppliers.GetSuppliers();
        return Ok(suppliers);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupplier(int id)
    {
        var supplier = await Suppliers.GetSupplier(id);
        if (supplier == null)
        {
            return BadRequest($"supplier with ID {id} not found");
        }
        return Ok(supplier);
    }
    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsForSupplier(int id)
    {
        var items = await Suppliers.GetItemsForSupplier(id);
        if (items == null)
        {
            return BadRequest($"no items found for supplier with ID {id}");
        }
        return Ok(items);
    }
    [HttpPost()]
    public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
    {
        var newSupplier = await Suppliers.CreateSupplier(supplier);
        if (newSupplier == null)
        {
            return BadRequest("failed to create supplier");
        }
        return Created();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await Suppliers.DeleteSupplier(id);
        if (supplier == null)
        {
            return BadRequest($"supplier with ID {id} not found");
        }
        return Ok(supplier);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
    {
        var oldSupplier = await Suppliers.UpdateSupplier(id, supplier);
        if (oldSupplier == null)
        {
            return BadRequest($"supplier with ID {id} not found");
        }
        return Ok(oldSupplier);
    }
}