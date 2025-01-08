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
        public required int Id { get; set; }
        public required int SourceId { get; set; }
        public required string? OrderDate { get; set; }
        public required string? RequestDate { get; set; }
        public required string? Reference { get; set; }
        public required string? ReferenceExtra { get; set; }
        public required string? OrderStatus { get; set; }
        public required string? Notes { get; set; }
        public required string? ShippingNotes { get; set; }
        public required string? PickingNotes { get; set; }
        public required int WarehouseId { get; set; }
        public required int ShipTo { get; set; }
        public required int BillTo { get; set; }
        public required int ShipmentId { get; set; }
        public required double TotalAmount { get; set; }
        public required double TotalDiscount { get; set; }
        public required double TotalTax { get; set; }
        public required double TotalSurcharge { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();
        public List<OrderedItem> Items { get; set; } = new List<OrderedItem>();

        public Order() { }

        public Order(int id, int sourceId, string orderDate, string requestDate, string reference, string referenceExtra, string orderStatus, string notes, string shippingNotes, string pickingNotes, int warehouseId, int shipTo, int billTo, int shipmentId, double totalAmount, double totalDiscount, double totalTax, double totalSurcharge, List<OrderedItem>? items)
        {
            Id = id;
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
