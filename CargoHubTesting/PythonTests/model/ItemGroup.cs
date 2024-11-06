namespace PythonTests.models;

public class item_group
{
    public int id {get; set;}
    public string name {get; set;}
    public string description {get; set;}

    public item_group (int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }
}