using Microsoft.AspNetCore.Mvc;
[Route("api/v1")]
public class SupplierController : Controller
{
    private ISuppliers Suppliers { get; set; }
    public SupplierController(ISuppliers suppliers)
    {
        Suppliers = suppliers;
    }
    
}