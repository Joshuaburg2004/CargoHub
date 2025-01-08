using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonOrder : JsonBase{
        public int id { get; set; }
        public int source_id { get; set; }
        public string? order_date { get; set; }
        public string? request_date { get; set; }
        public string? reference { get; set; }
        public string? reference_extra { get; set; }
        public string? order_status { get; set; }
        public string? notes { get; set; }
        public string? shipping_notes { get; set; }
        public string? picking_notes { get; set; }
        public int warehouse_id { get; set; }
        public int ship_to { get; set; }
        public int bill_to { get; set; }
        public int shipment_id { get; set; }
        public double total_amount { get; set; }
        public double total_discount { get; set; }
        public double total_tax { get; set; }
        public double total_surcharge { get; set; }

        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();

        public Order ToOrder(){
            return new Order(){
                Id = id,
                SourceId = source_id,
                OrderDate = order_date,
                RequestDate = request_date,
                Reference = reference,
                ReferenceExtra = reference_extra,
                OrderStatus = order_status,
                Notes = notes,
                ShippingNotes = shipping_notes,
                PickingNotes = picking_notes,
                WarehouseId = warehouse_id,
                ShipTo = ship_to,
                BillTo = bill_to,
                ShipmentId = shipment_id,
                TotalAmount = total_amount,
                TotalDiscount = total_discount,
                TotalTax = total_tax,
                TotalSurcharge = total_surcharge,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}