using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Supplier : Base
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address_Extra { get; set; }
        public string City { get; set; }
        public string Zip_Code { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Contact_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public string Created_At { get; set; } = GetTimeStamp();
        public string Updated_At { get; set; } = GetTimeStamp();
        public Supplier(int id, string code, string name, string address, string address_Extra, string city, string zip_Code, string province, string country, string contact_Name, string phoneNumber, string reference)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Address_Extra = address_Extra;
            City = city;
            Zip_Code = zip_Code;
            Province = province;
            Country = country;
            Contact_Name = contact_Name;
            PhoneNumber = phoneNumber;
            Reference = reference;
        }
    }
}