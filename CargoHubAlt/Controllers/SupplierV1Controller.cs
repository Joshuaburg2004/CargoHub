using Microsoft.AspNetCore.Mvc;
[Route("api/v1/suppliers")]
public class SupplierV1Controller : Controller{
    private ISuppliers Suppliers { get; set; }
    public SupplierV1Controller(ISuppliers suppliers){
        Suppliers = suppliers;
    }
    [HttpGet()]
    public async Task<IActionResult> GetAllSuppliers() => Ok(await Suppliers.GetAllSuppliers());
    [HttpGet()]
    public async Task<IActionResult> GetOneSupplier([FromQuery] Guid id) => Ok(await Suppliers.GetOneSupplier(id));
    [HttpPost()]
    public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier) => Ok(await Suppliers.CreateSupplier(supplier));
    [HttpPut()]
    public async Task<IActionResult> UpdateSupplier([FromQuery] Guid id, [FromBody] Supplier supplier) => Ok(await Suppliers.UpdateSupplier(id, supplier));
    [HttpDelete()]
    public async Task<IActionResult> DeleteSupplier([FromQuery] Guid id) => Ok(await Suppliers.DeleteSupplier(id));
    /*
    // commented out because Item is not done yet
    [HttpGet()]
    public async Task<IActionResult> GetItemsForSupplier([FromQuery] Guid id) => Ok(await Suppliers.GetItemsForSupplier(id));
    */
}