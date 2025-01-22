using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonLocation : JsonBase{
        public int id { get; set; }
        public int warehouse_id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();

        public Location ToLocation(){
            return new Location(){
                Id = id,
                Name = name,
                Code = code,
                WarehouseId = warehouse_id,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}