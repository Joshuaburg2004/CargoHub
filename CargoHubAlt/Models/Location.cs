using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Location(int warehouseId, string code, string name)
        {
            WarehouseId = warehouseId;
            Code = code;
            Name = name;
        }
    }
}