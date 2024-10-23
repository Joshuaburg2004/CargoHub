namespace CargoHubAlt.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public Contact Contact { get; set; }

        public Warehouse(string code, string name, string address, string zip, string city, string province, string country, Contact contact)
        {
            Id = Guid.NewGuid();
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

    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public Contact(string name, string phone, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Phone = phone;
            Email = email;
            CreatedAt = GetTimeStamp();
            UpdatedAt = GetTimeStamp();
        }
    }
}