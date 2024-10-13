public class Warehouse
{
    public Guid id { get; set; }
    public string code { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string zip { get; set; }
    public string city { get; set; }
    public string province { get; set; }
    public string country { get; set; }
    public Contact contact { get; set; }
}

public class Contact
{
    public string name { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
}

