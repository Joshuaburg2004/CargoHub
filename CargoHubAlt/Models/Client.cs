using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("Id")]
    public class Client : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip_Code { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Phone { get; set; }
        public string Contact_Email { get; set; }
        public string Created_At { get; set; } = GetTimeStamp();
        public string Updated_At { get; set; } = GetTimeStamp();
        public Client(int id, string name, string address, string city, string zip_Code, string province, string country, string contact_Name, string contact_Phone, string contact_Email)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.City = city;
            this.Zip_Code = zip_Code;
            this.Province = province;
            this.Country = country;
            this.Contact_Name = contact_Name;
            this.Contact_Phone = contact_Phone;
            this.Contact_Email = contact_Email;
        }
    }
}
