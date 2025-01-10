using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonShipment : JsonBase{
        public int id { get; set; }
        public int order_id { get; set; }
        public int source_id { get; set; }
        public string? order_date { get; set; }
        public string? request_date { get; set; }
        public string? shipment_date { get; set; }
        public string? shipment_type { get; set; }
        public string? shipment_status { get; set; }
        public string? notes { get; set; }
        public string? carrier_code { get; set; }
        public string? carrier_description { get; set; }
        public string? service_code { get; set; }
        public string? payment_type { get; set; }
        public string? transfer_mode { get; set; }
        public int total_package_count { get; set; }
        public double total_package_weight { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public List<JsonShipmentItem> Items { get; set; } = new();
        public Shipment ToShipment(){
            return new Shipment(){
                Id = id,
                OrderId = order_id,
                SourceId = source_id,
                OrderDate = order_date,
                RequestDate = request_date,
                ShipmentDate = shipment_date,
                ShipmentType = shipment_type,
                ShipmentStatus = shipment_status,
                Notes = notes,
                CarrierCode = carrier_code,
                CarrierDescription = carrier_description,
                ServiceCode = service_code,
                PaymentType = payment_type,
                TransferMode = transfer_mode,
                TotalPackageCount = total_package_count,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
    public class JsonShipmentItem{
        public string? item_id { get; set; }
        public int amount { get; set; }
        public ShipmentItem ToShipmentItem(){
            return new ShipmentItem(){
                ItemId = item_id,
                Amount = amount
            };
        }
    }
}