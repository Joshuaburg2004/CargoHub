using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Location : Base
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();

        public Location(int id, int warehouseId, string code, string name)
        {
            Id = id;
            WarehouseId = warehouseId;
            Code = code;
            Name = name;
        }
    }
}