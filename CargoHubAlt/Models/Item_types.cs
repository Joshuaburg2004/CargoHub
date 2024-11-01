public class Item_type : Base
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Item_type(int id, string name, string description)
    {
        Id = id;
        this.Name = name;
        this.Description = description;
    }
}