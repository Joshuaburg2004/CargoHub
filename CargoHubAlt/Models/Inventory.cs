using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Inventory : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string ItemId { get; set; }
        public required string Description { get; set; }
        public required string ItemReference { get; set; }
        public required List<int> Locations { get; set; }
        public required int TotalOnHand { get; set; }
        public required int TotalExpected { get; set; }
        public required int TotalOrdered { get; set; }
        public required int TotalAllocated { get; set; }
        public required int TotalAvailable { get; set; }
        public required int? LowStockThreshold { get; set; }
        public bool? IsLowStock { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();
        public Inventory() { }
        public Inventory(int id, string itemId, string description, string itemReference, List<int> locations, int totalOnHand, int totalExpected, int totalOrdered, int totalAllocated, int totalAvailable, int lowStockThreshold)
        {
            Id = id;
            ItemId = itemId;
            Description = description;
            ItemReference = itemReference;
            Locations = locations;
            TotalOnHand = totalOnHand;
            TotalExpected = totalExpected;
            TotalOrdered = totalOrdered;
            TotalAllocated = totalAllocated;
            TotalAvailable = totalAvailable;
            LowStockThreshold = lowStockThreshold;
            IsLowStock = LowStockThreshold.HasValue ? TotalOnHand <= LowStockThreshold : false;
        }
        public Inventory(string itemId, string description, string itemReference, List<int> locations, int totalOnHand, int totalExpected, int totalOrdered, int totalAllocated, int totalAvailable, int lowStockThreshold)
        {
            ItemId = itemId;
            Description = description;
            ItemReference = itemReference;
            Locations = locations;
            TotalOnHand = totalOnHand;
            TotalExpected = totalExpected;
            TotalOrdered = totalOrdered;
            TotalAllocated = totalAllocated;
            TotalAvailable = totalAvailable;
            LowStockThreshold = lowStockThreshold;
            IsLowStock = LowStockThreshold.HasValue ? TotalOnHand <= LowStockThreshold : false;
        }
    }
}
