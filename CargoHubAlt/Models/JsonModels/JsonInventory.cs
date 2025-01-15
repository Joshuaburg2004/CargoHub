using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonInventory : JsonBase{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? item_id { get; set; }
        public string? description { get; set; }
        public string? item_reference { get; set; }
        public List<int> locations { get; set; } = new List<int>();
        public int total_on_hand { get; set; }
        public int total_expected { get; set; }
        public int total_ordered { get; set; }
        public int total_allocated { get; set; }
        public int total_available { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public Inventory ToInventory(){
            return new Inventory(){
                Id = id,
                ItemId = item_id,
                Description = description,
                ItemReference = item_reference,
                Locations = locations,
                TotalOnHand = total_on_hand,
                TotalExpected = total_expected,
                TotalOrdered = total_ordered,
                TotalAllocated = total_allocated,
                TotalAvailable = total_available,
                CreatedAt = created_at,
                UpdatedAt = updated_at,
            };
        }
    }
}