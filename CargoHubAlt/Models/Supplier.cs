using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Supplier : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string AddressExtra { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public required string Province { get; set; }
        public required string Country { get; set; }
        public required string ContactName { get; set; }
        public required string Phonenumber { get; set; }
        public required string Reference { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();
        public Supplier() { }
        public Supplier(int id, string code, string name, string address, string addressExtra, string city, string zipCode, string province, string country, string contactName, string phonenumber, string reference)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            AddressExtra = addressExtra;
            City = city;
            ZipCode = zipCode;
            Province = province;
            Country = country;
            ContactName = contactName;
            Phonenumber = phonenumber;
            Reference = reference;
        }
        public Supplier(string code, string name, string address, string addressExtra, string city, string zipCode, string province, string country, string contactName, string phonenumber, string reference)
        {
            Code = code;
            Name = name;
            Address = address;
            AddressExtra = addressExtra;
            City = city;
            ZipCode = zipCode;
            Province = province;
            Country = country;
            ContactName = contactName;
            Phonenumber = phonenumber;
            Reference = reference;
        }
    }
}