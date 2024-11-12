using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Item_type : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }

    public string Description { get; set; }
    public string Created_At { get; set; } = GetTimeStamp();
    public string Updated_At { get; set; } = GetTimeStamp();

        public Item_type(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}