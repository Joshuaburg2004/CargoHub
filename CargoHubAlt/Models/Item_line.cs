
public class Item_line : Base
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Item_line(string name, string Description)
    {
        this.Id = new Guid();
        this.Name = name;
        this.Description = Description;
    }
}