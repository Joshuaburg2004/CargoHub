using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonSupplier : JsonBase{
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? address_extra { get; set; }
        public string? city { get; set; }
        public string? zip_code { get; set; }
        public string? province { get; set; }
        public string? country { get; set; }
        public string? contact_name { get; set; }
        public string? phonenumber { get; set; }
        public string? reference { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public Supplier ToSupplier(){
            return new Supplier(){
                Id = id,
                Code = code,
                Name = name,
                Address = address,
                AddressExtra = address_extra,
                City = city,
                ZipCode = zip_code,
                Province = province,
                Country = country,
                ContactName = contact_name,
                Phonenumber = phonenumber,
                Reference = reference,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
}