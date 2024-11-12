using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Warehouse : Base
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public required Contact Contact { get; set; }
        public string? CreatedAt { get; set; } = Base.GetTimeStamp();
        public string? UpdatedAt { get; set; } = Base.GetTimeStamp();
        public Warehouse() { }

        public Warehouse(int id, string code, string name, string address, string zip, string city, string province, string country, Contact contact)
        {
            Id = id;
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