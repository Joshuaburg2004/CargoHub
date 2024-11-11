using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Item_line : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();

        public Item_line(int id, string name, string Description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = Description;
        }
    }
}