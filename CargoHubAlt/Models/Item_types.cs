public class Item_type : Base
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Item_type(string name, string Description)
    {
        Id = Guid.NewGuid();
        this.Name = name;
        this.Description = Description;
    }
}