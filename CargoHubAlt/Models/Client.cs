using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class Client : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();
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
