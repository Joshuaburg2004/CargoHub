using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("uid")]
    public class Item : Base
    {
        public string uid { get; set; }
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


        public Item(string uid, string code, string description, string short_description, string upc_code, string model_number, string commodity_code, int item_line, int item_group, int item_type, int unit_purchase_quantity, int unit_order_quantity, int pack_order_quantity, int supplier_id, string supplier_code, string supplier_part_number)
        {
            this.uid = uid;
            this.code = code;
            this.description = description;
            this.short_description = short_description;
            this.upc_code = upc_code;
            this.model_number = model_number;
            this.commodity_code = commodity_code;
            this.item_line = item_line;
            this.item_group = item_group;
            this.item_type = item_type;
            this.unit_purchase_quantity = unit_purchase_quantity;
            this.unit_order_quantity = unit_order_quantity;
            this.pack_order_quantity = pack_order_quantity;
            this.supplier_id = supplier_id;
            this.supplier_code = supplier_code;
            this.supplier_part_number = supplier_part_number;
        }
    }
}