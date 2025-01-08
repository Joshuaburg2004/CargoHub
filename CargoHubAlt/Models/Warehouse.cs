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
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public Contact Contact { get; set; } = new Contact("", "", "");
        public string CreatedAt { get; set; } = Base.GetTimeStamp();
        public string UpdatedAt { get; set; } = Base.GetTimeStamp();
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