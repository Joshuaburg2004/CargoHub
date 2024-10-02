public record Client{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Address {get; set;}
    public string City {get; set;}
    public string State {get; set;}
    public string Zip {get; set;}
    public string Country {get; set;}
    public string ContactName {get; set;}
    public string ContactPhone {get; set;}
    public string ContactEmail {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public Client(int id, string name, string address, string city, string zipCode, string province, string country, string contactName, string contactPhone, string contactEmail){
        Id = id;
        Name = name;
        Address = address;
        City = city;
        State = province;
        Zip = zipCode;
        Country = country;
        ContactName = contactName;
        ContactPhone = contactPhone;
        ContactEmail = contactEmail;
    }
}