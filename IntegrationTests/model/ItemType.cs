namespace IntegrationTests.models;

public class item_type
{
    public int id {get; set;}
    public string name {get; set;}
    public string description {get; set;}

    public item_type (int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }
}