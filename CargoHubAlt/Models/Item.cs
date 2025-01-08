using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Uid")]
    public class Item : Base
    {
        public required string Uid { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required string UpcCode { get; set; }
        public required string ModelNumber { get; set; }
        public required string CommodityCode { get; set; }
        public required int ItemLine { get; set; }
        public required int ItemGroup { get; set; }
        public required int ItemType { get; set; }
        public required int UnitPurchaseQuantity { get; set; }
        public required int UnitOrderQuantity { get; set; }
        public required int PackOrderQuantity { get; set; }
        public required int SupplierId { get; set; }
        public required string SupplierCode { get; set; }
        public required string SupplierPartNumber { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();


        public Item(string uid, string code, string description, string shortDescription, string upcCode, string modelNumber, string commodityCode, int itemLine, int itemGroup, int itemType, int unitPurchaseQuantity, int unitOrderQuantity, int packOrderQuantity, int supplierId, string supplierCode, string supplierPartNumber)
        {
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
}