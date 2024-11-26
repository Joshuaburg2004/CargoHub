using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Supplier : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressExtra { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string Phonenumber { get; set; }
        public string Reference { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();
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