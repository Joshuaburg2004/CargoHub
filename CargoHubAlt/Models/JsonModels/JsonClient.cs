using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonClient : JsonBase{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? zip_code { get; set; }
        public string? province { get; set; }
        public string? country { get; set; }
        public string? contact_name { get; set; }
        public string? contact_phone { get; set; }
        public string? contact_email { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public Client ToClient(){
            return new Client{
                Id = id,
                Name = name,
                Address = address,
                City = city,
                ZipCode = zip_code,
                Province = province,
                Country = country,
                ContactName = contact_name,
                ContactPhone = contact_phone,
                ContactEmail = contact_email,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}