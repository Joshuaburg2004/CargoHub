using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Order : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string? OrderDate { get; set; }
        public string? RequestDate { get; set; }
        public string? Reference { get; set; }
        public string? ReferenceExtra { get; set; }
        public string? OrderStatus { get; set; }
        public string? Notes { get; set; }
        public string? ShippingNotes { get; set; }
        public string? PickingNotes { get; set; }
        public int WarehouseId { get; set; }
        public int ShipTo { get; set; }
        public int BillTo { get; set; }
        public int ShipmentId { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalTax { get; set; }
        public double TotalSurcharge { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();
        public List<OrderedItem> Items { get; set; } = new List<OrderedItem>();

        public Order() { }

        public Order(int sourceId, string orderDate, string requestDate, string reference, string referenceExtra, string orderStatus, string notes, string shippingNotes, string pickingNotes, int warehouseId, int shipTo, int billTo, int shipmentId, double totalAmount, double totalDiscount, double totalTax, double totalSurcharge, List<OrderedItem>? items)
        {
            SourceId = sourceId;
            OrderDate = orderDate;
            RequestDate = requestDate;
            Reference = reference;
            ReferenceExtra = referenceExtra;
            OrderStatus = orderStatus;
            Notes = notes;
            ShippingNotes = shippingNotes;
            PickingNotes = pickingNotes;
            WarehouseId = warehouseId;
            ShipTo = shipTo;
            BillTo = billTo;
            ShipmentId = shipmentId;
            TotalAmount = totalAmount;
            TotalDiscount = totalDiscount;
            TotalTax = totalTax;
            TotalSurcharge = totalSurcharge;
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
        public string? ItemId { get; set; }
        public int Amount { get; set; }
    }

}
