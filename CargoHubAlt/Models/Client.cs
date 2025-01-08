using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Client : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public required string Province { get; set; }
        public required string Country { get; set; }
        public required string ContactName { get; set; }
        public required string ContactPhone { get; set; }
        public required string ContactEmail { get; set; }
        public required string CreatedAt { get; set; } = GetTimeStamp();
        public required string UpdatedAt { get; set; } = GetTimeStamp();
        public Client() { }
        public Client(int id, string name, string address, string city, string zipCode, string province, string country, string contactName, string contactPhone, string contactEmail)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            ZipCode = zipCode;
            Province = province;
            Country = country;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
        }
        public Client(string name, string address, string city, string zipCode, string province, string country, string contactName, string contactPhone, string contactEmail)
        {
            Name = name;
            Address = address;
            City = city;
            ZipCode = zipCode;
            Province = province;
            Country = country;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
        }
    }
}
