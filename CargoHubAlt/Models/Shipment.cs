using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Shipment : Base
{
    public int Id { get; set; }
    public int Order_Id { get; set; } // maybe this should be a list of order ids
    public int Source_Id { get; set; }
    public string? Order_Date { get; set; }
    public string? Request_Date { get; set; }
    public string? Shipment_Date { get; set; }
    public string? Shipment_Type { get; set; }
    public string? Shipment_Status { get; set; }
    public string? Notes { get; set; }
    public string? Carrier_Code { get; set; }
    public string? Carrier_Description { get; set; }
    public string? Service_Code { get; set; }
    public string? Payment_Type { get; set; }
    public string? Transfer_Mode { get; set; }
    public int Total_Package_Count { get; set; }
    public double Total_Package_Weight { get; set; }
    public string? Created_At { get; set; } = GetTimeStamp();
    public string? Updated_At { get; set; } = GetTimeStamp();
    public List<ShipmentItem>? Items { get; set; } = new List<ShipmentItem>();

    public Shipment() { }
    public Shipment(int id, int orderId, int sourceId, string orderDate, string requestDate, string shipmentDate, string shipmentType, string shipmentStatus, string notes, string carrierCode, string carrierDescription, string serviceCode, string paymentType, string transferMode, int totalPackageCount, int totalPackageWeight, string createdAt, string updatedAt, List<ShipmentItem> items)
    {
        Id = id;
        Order_Id = orderId;
        Source_Id = sourceId;
        Order_Date = orderDate;
        Request_Date = requestDate;
        Shipment_Date = shipmentDate;
        Shipment_Type = shipmentType;
        Shipment_Status = shipmentStatus;
        Notes = notes;
        Carrier_Code = carrierCode;
        Carrier_Description = carrierDescription;
        Service_Code = serviceCode;
        Payment_Type = paymentType;
        Transfer_Mode = transferMode;
        Total_Package_Count = totalPackageCount;
        Total_Package_Weight = totalPackageWeight;
        Created_At = createdAt;
        Updated_At = updatedAt;
        Items = items;
    }
}

[Owned]
public class ShipmentItem
{
    [Key]
    public string? Item_Id { get; set; }
    public int Amount { get; set; }
}