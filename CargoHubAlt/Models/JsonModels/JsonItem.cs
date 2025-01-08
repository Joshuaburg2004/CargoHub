using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonItem: JsonBase{
        public required string uid { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string short_description { get; set; }
        public string upc_code { get; set; }
        public string model_number { get; set; }
        public string commodity_code { get; set; }
        public int item_line { get; set; }
        public int item_group { get; set; }
        public int item_type { get; set; }
        public int unit_purchase_quantity { get; set; }
        public int unit_order_quantity { get; set; }
        public int pack_order_quantity { get; set; }
        public int supplier_id { get; set; }
        public string supplier_code { get; set; }
        public string supplier_part_number { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public JsonItem(string uid, string code, string description, string shortDescription, string upcCode, string modelNumber, string commodityCode, int itemLine, int itemGroup, int itemType, int unitPurchaseQuantity, int unitOrderQuantity, int packOrderQuantity, int supplierId, string supplierCode, string supplierPartNumber)
        {
            this.uid = uid;
            this.code = code;
            this.description = description;
            this.short_description = shortDescription;
            this.upc_code = upcCode;
            this.model_number = modelNumber;
            this.commodity_code = commodityCode;
            this.item_line = itemLine;
            this.item_group = itemGroup;
            this.item_type = itemType;
            this.unit_purchase_quantity = unitPurchaseQuantity;
            this.unit_order_quantity = unitOrderQuantity;
            this.pack_order_quantity = packOrderQuantity;
            this.supplier_id = supplierId;
            this.supplier_code = supplierCode;
            this.supplier_part_number = supplierPartNumber;
        }
        public Item ToItem(){
            return new Item(uid, code, description, short_description, upc_code, model_number, commodity_code, item_line, item_group, item_type, unit_purchase_quantity, unit_order_quantity, pack_order_quantity, supplier_id, supplier_code, supplier_part_number){
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}