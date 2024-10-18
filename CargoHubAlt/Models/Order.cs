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

    public Order(int sourceId, string reference, string referenceExtra, string orderStatus, string noted, string shippingNoted, string pickingNotes, double totalAmount, double totalDiscount, double totalTax, double totalSurcharge)
    {
        SourceId = sourceId;
        Reference = reference;
        ReferenceExtra = referenceExtra;
        OrderStatus = orderStatus;
        Notes = noted;
        ShippingNotes = shippingNoted;
        PickingNotes = pickingNotes;
        TotalAmount = totalAmount;
        TotalDiscount = totalDiscount;
        TotalTax = totalTax;
        TotalSurcharge = totalSurcharge;
    }
}
