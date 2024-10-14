using Microsoft.AspNetCore.Mvc;
[Route("api/v2/suppliers")]
public class SupplierV2Controller : Controller{
    private ISuppliers Suppliers { get; set; }
    public SupplierV2Controller(ISuppliers suppliers){
        Suppliers = suppliers;
    }
    [HttpGet()]
    public async Task<IActionResult> GetBatchSuppliers([FromQuery] Guid[] guids) => Ok(await Suppliers.GetBatchSuppliers(guids));
}
