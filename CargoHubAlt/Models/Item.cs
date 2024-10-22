public class Item : Base
{
    public Guid Id { get; set; }
    public string Uid { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string UpcCode { get; set; }
    public string ModelNumber { get; set; }
    public string CommodityCode { get; set; }
    public Guid ItemLine { get; set; }
    public Guid ItemGroup { get; set; }
    public Guid ItemType { get; set; }
    public int UnitPurchaseQuantity { get; set; }
    public int UnitOrderQuantity { get; set; }
    public int PackOrderQuantity { get; set; }
    public Guid SupplierId { get; set; }
    public string SupplierCode { get; set; }
    public string SupplierPartNumber { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Item() { }
    public Item(string uid, string code, string description, string shortDescription, string upcCode, string modelNumber, string commodityCode, Guid itemLine, Guid itemGroup, Guid itemType, int unitPurchaseQuantity, int unitOrderQuantity, int packOrderQuantity, Guid supplierId, string supplierCode, string supplierPartNumber)
    {
        Id = Guid.NewGuid();
        Uid = uid;
        Code = code;
        Description = description;
        ShortDescription = shortDescription;
        UpcCode = upcCode;
        ModelNumber = modelNumber;
        CommodityCode = commodityCode;
        ItemLine = itemLine;
        ItemGroup = itemGroup;
        ItemType = itemType;
        UnitPurchaseQuantity = unitPurchaseQuantity;
        UnitOrderQuantity = unitOrderQuantity;
        PackOrderQuantity = packOrderQuantity;
        SupplierId = supplierId;
        SupplierCode = supplierCode;
        SupplierPartNumber = supplierPartNumber;
    }
}