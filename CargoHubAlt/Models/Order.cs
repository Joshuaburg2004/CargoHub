public class Order : Base
{
    public Guid Id { get; set; }
    public int SourceId { get; set; }
    public string Reference { get; set; }
    public string ReferenceExtra { get; set; }
    public string OrderStatus { get; set; }
    public string Notes { get; set; }
    public string ShippingNotes { get; set; }
    public string PickingNotes { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ShipTo { get; set; }
    public Guid BillTo { get; set; }
    public Guid ShipmentId { get; set; }
    public double TotalAmount { get; set; }
    public double TotalDiscount { get; set; }
    public double TotalTax { get; set; }
    public double TotalSurcharge { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();
    public List<OrderedItem> Items { get; set; } = new List<OrderedItem>();
}
