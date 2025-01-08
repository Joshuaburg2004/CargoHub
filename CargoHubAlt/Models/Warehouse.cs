using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Warehouse : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Zip { get; set; }
        public required string City { get; set; }
        public required string Province { get; set; }
        public required string Country { get; set; }
        public required Contact Contact { get; set; }
        public required string CreatedAt { get; set; } = Base.GetTimeStamp();
        public required string UpdatedAt { get; set; } = Base.GetTimeStamp();
        public Warehouse() { }

        public Warehouse(int id, string code, string name, string address, string zip, string city, string province, string country, Contact contact)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Zip = zip;
            City = city;
            Province = province;
            Country = country;
            Contact = contact;
        }
        public Warehouse(string code, string name, string address, string zip, string city, string province, string country, Contact contact)
        {
            Code = code;
            Name = name;
            Address = address;
            Zip = zip;
            City = city;
            Province = province;
            Country = country;
            Contact = contact;
        }
    }
    [Owned]
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Contact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}