using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;

namespace CargoHubAlt.Models
{
    public class Shipment
    {
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
        public int TotalPackageWeight { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public List<ShipmentItem>? Items { get; set; }

        public Shipment() { }
        public Shipment(int id, int orderId, int sourceId, string orderDate, string requestDate, string shipmentDate, string shipmentType, string shipmentStatus, string notes, string carrierCode, string carrierDescription, string serviceCode, string paymentType, string transferMode, int totalPackageCount, int totalPackageWeight, string createdAt, string updatedAt, List<ShipmentItem> items)
        {
            Id = id;
            OrderId = orderId;
            SourceId = sourceId;
            OrderDate = orderDate;
            RequestDate = requestDate;
            ShipmentDate = shipmentDate;
            ShipmentType = shipmentType;
            ShipmentStatus = shipmentStatus;
            Notes = notes;
            CarrierCode = carrierCode;
            CarrierDescription = carrierDescription;
            ServiceCode = serviceCode;
            PaymentType = paymentType;
            TransferMode = transferMode;
            TotalPackageCount = totalPackageCount;
            TotalPackageWeight = totalPackageWeight;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Items = items;
        }
    }
    [Owned]    
    public class ShipmentItem
    {
        public string? Item_Id { get; set; }
        public int Amount { get; set; }
    }
}