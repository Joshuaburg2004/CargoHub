using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Order : Base
    {
        public int Id { get; set; }
        public int Source_Id { get; set; }
        public string? Order_Date { get; set; }
        public string? Request_Date { get; set; }
        public string? Reference { get; set; }
        public string? Reference_Extra { get; set; }
        public string? Order_Status { get; set; }
        public string? Notes { get; set; }
        public string? Shipping_Notes { get; set; }
        public string? Picking_Notes { get; set; }
        public int Warehouse_Id { get; set; }
        public int Ship_To { get; set; }
        public int Bill_To { get; set; }
        public int Shipment_Id { get; set; }
        public double Total_Amount { get; set; }
        public double Total_Discount { get; set; }
        public double Total_Tax { get; set; }
        public double Total_Surcharge { get; set; }
        public string Created_At { get; set; } = GetTimeStamp();
        public string Updated_At { get; set; } = GetTimeStamp();
        public List<OrderedItem> Items { get; set; } = new List<OrderedItem>();

        public Order() { }

        public Order(int id, int sourceId, string orderDate, string requestDate, string reference, string referenceExtra, string orderStatus, string notes, string shippingNotes, string pickingNotes, int warehouseId, int shipTo, int billTo, int shipmentId, double totalAmount, double totalDiscount, double totalTax, double totalSurcharge, List<OrderedItem>? items)
        {
            Id = id;
            Source_Id = sourceId;
            Order_Date = orderDate;
            Request_Date = requestDate;
            Reference = reference;
            Reference_Extra = referenceExtra;
            Order_Status = orderStatus;
            Notes = notes;
            Shipping_Notes = shippingNotes;
            Picking_Notes = pickingNotes;
            Warehouse_Id = warehouseId;
            Ship_To = shipTo;
            Bill_To = billTo;
            Shipment_Id = shipmentId;
            Total_Amount = totalAmount;
            Total_Discount = totalDiscount;
            Total_Tax = totalTax;
            Total_Surcharge = totalSurcharge;
            // If items is not null and has items, then assign it to Items else keep it as empty list as done in the field declaration.
            if (items != null && items.Count > 0)
            {
                Items = items;
            }
        }
    }

    [Owned]
    public class OrderedItem
    {
        [Key]
        public string? Item_Id { get; set; }
        public int Amount { get; set; }
    }
}
