using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class ItemLine : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();
        public ItemLine() { }
        public ItemLine(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public ItemLine(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}