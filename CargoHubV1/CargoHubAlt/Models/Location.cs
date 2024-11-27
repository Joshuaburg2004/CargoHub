using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CargoHubAlt.Models
{
    public class Location : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();
        public Location() { }
        public Location(int id, int warehouseId, string code, string name)
        {
            Id = id;
            WarehouseId = warehouseId;
            Code = code;
            Name = name;
        }
        public Location(int warehouseId, string code, string name)
        {
            WarehouseId = warehouseId;
            Code = code;
            Name = name;
        }
    }
}