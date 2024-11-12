using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    [PrimaryKey("id")]
    public class Client : Base
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string contact_name { get; set; }
        public string contact_phone { get; set; }
        public string contact_email { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public Client(int id, string name, string address, string city, string zip_code, string province, string country, string contact_name, string contact_phone, string contact_email)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.city = city;
            this.zip_code = zip_code;
            this.province = province;
            this.country = country;
            this.contact_name = contact_name;
            this.contact_phone = contact_phone;
            this.contact_email = contact_email;
        }
    }
}
