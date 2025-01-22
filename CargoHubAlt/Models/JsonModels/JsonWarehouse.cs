using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonWarehouse : JsonBase{
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? zip { get; set; }
        public string? city { get; set; }
        public string? province { get; set; }
        public string? country { get; set; }
        public required JsonContact contact { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public Warehouse ToWarehouse(){
            return new Warehouse(){
                Id = id,
                Code = code,
                Name = name,
                Address = address,
                City = city,
                Zip = zip,
                Province = province,
                Country = country,
                Contact = contact.ToContact(),
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
    public class JsonContact{
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public JsonContact(string name, string phone, string email){
            this.name = name;
            this.phone = phone;
            this.email = email;
        }
        public Contact ToContact(){
            return new Contact(name, phone, email);
        }
    }
}