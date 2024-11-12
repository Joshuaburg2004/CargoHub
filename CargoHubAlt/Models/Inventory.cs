using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Inventory : Base
    {
        public int Id { get; set; }
        public string Item_id { get; set; }
        public string Description { get; set; }
        public string Item_reference { get; set; }
        public List<int> Locations { get; set; }
        public int Total_on_hand { get; set; }
        public int Total_expected { get; set; }
        public int Total_ordered { get; set; }
        public int Total_allocated { get; set; }
        public int Total_available { get; set; }
        public string Created_at { get; set; } = GetTimeStamp();
        public string Updated_at { get; set; } = GetTimeStamp();

        public Inventory(int id, string item_id, string description, string item_reference, List<int> locations, int total_on_hand, int total_expected, int total_ordered, int total_allocated, int total_available)
        {
            this.Id = id;
            this.Item_id = item_id;
            this.Description = description;
            this.Item_reference = item_reference;
            this.Locations = locations;
            this.Total_on_hand = total_on_hand;
            this.Total_expected = total_expected;
            this.Total_ordered = total_ordered;
            this.Total_allocated = total_allocated;
            this.Total_available = total_available;
        }
    }
}
