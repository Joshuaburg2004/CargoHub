using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("id")]
    public class Inventory : Base
    {
        public int id { get; set; }
        public string item_id { get; set; }
        public string description { get; set; }
        public string item_reference { get; set; }
        public List<int> locations { get; set; }
        public int total_on_hand { get; set; }
        public int total_expected { get; set; }
        public int total_ordered { get; set; }
        public int total_allocated { get; set; }
        public int total_available { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();

        public Inventory(int id, string item_id, string description, string item_reference, List<int> locations, int total_on_hand, int total_expected, int total_ordered, int total_allocated, int total_available)
        {
            this.id = id;
            this.item_id = item_id;
            this.description = description;
            this.item_reference = item_reference;
            this.locations = locations;
            this.total_on_hand = total_on_hand;
            this.total_expected = total_expected;
            this.total_ordered = total_ordered;
            this.total_allocated = total_allocated;
            this.total_available = total_available;
        }
    }
}
