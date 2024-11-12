using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("id")]
    public class Location : Base
    {
        public int id { get; set; }
        public int warehouse_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();

        public Location(int id, int warehouse_id, string code, string name)
        {
            this.id = id;
            this.warehouse_id = warehouse_id;
            this.code = code;
            this.name = name;
        }
    }
}