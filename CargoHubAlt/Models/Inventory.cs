using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Inventory : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ItemId { get; set; }
        public string? Description { get; set; }
        public string? ItemReference { get; set; }
        public List<int> Locations { get; set; } = new List<int>();
        public int TotalOnHand { get; set; }
        public int TotalExpected { get; set; }
        public int TotalOrdered { get; set; }
        public int TotalAllocated { get; set; }
        public int TotalAvailable { get; set; }
        public int? LowStockThreshold { get; set; }
        public bool? IsLowStock { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();
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
            IsLowStock = LowStockThreshold.HasValue ? totalAvailable <= LowStockThreshold : false;
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
            IsLowStock = LowStockThreshold.HasValue ? totalAvailable <= LowStockThreshold : false;
        }
    }
}
