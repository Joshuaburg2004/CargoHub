using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonItemLine : JsonBase{
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();

        public ItemLine ToItemLine(){
            return new ItemLine(){
                Id = id,
                Name = name,
                Description = description,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}