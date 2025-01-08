using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Shipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; } // maybe this should be a list of order ids
        public int SourceId { get; set; }
        public string? OrderDate { get; set; }
        public string? RequestDate { get; set; }
        public string? ShipmentDate { get; set; }
        public string? ShipmentType { get; set; }
        public string? ShipmentStatus { get; set; }
        public string? Notes { get; set; }
        public string? CarrierCode { get; set; }
        public string? CarrierDescription { get; set; }
        public string? ServiceCode { get; set; }
        public string? PaymentType { get; set; }
        public string? TransferMode { get; set; }
        public int TotalPackageCount { get; set; }
        public double TotalPackageWeight { get; set; }
        public string CreatedAt { get; set; } = Base.GetTimeStamp();
        public string UpdatedAt { get; set; } = Base.GetTimeStamp();
        public  List<ShipmentItem> Items { get; set; } = new();

        public Shipment() { }
        public Shipment(int id, int order_id, int source_id, string order_date, string request_date, string shipment_date, string shipment_type, string shipment_status, string notes, string carrier_code, string carrier_description, string service_code, string payment_type, string transfer_mode, int total_package_count, double total_package_weight, List<ShipmentItem> items)
        {
            Id = id;
            OrderId = order_id;
            SourceId = source_id;
            OrderDate = order_date;
            RequestDate = request_date;
            ShipmentDate = shipment_date;
            ShipmentType = shipment_type;
            ShipmentStatus = shipment_status;
            Notes = notes;
            CarrierCode = carrier_code;
            CarrierDescription = carrier_description;
            ServiceCode = service_code;
            PaymentType = payment_type;
            TransferMode = transfer_mode;
            TotalPackageCount = total_package_count;
            TotalPackageWeight = total_package_weight;
            Items = items;
        }
        public Shipment(int order_id, int source_id, string order_date, string request_date, string shipment_date, string shipment_type, string shipment_status, string notes, string carrier_code, string carrier_description, string service_code, string payment_type, string transfer_mode, int total_package_count, double total_package_weight, List<ShipmentItem> items)
        {
            OrderId = order_id;
            SourceId = source_id;
            OrderDate = order_date;
            RequestDate = request_date;
            ShipmentDate = shipment_date;
            ShipmentType = shipment_type;
            ShipmentStatus = shipment_status;
            Notes = notes;
            CarrierCode = carrier_code;
            CarrierDescription = carrier_description;
            ServiceCode = service_code;
            PaymentType = payment_type;
            TransferMode = transfer_mode;
            TotalPackageCount = total_package_count;
            TotalPackageWeight = total_package_weight;
            Items = items;
        }
    }
    [Owned]
    public class ShipmentItem
    {
        public string? ItemId { get; set; }
        public int Amount { get; set; }
    }
}