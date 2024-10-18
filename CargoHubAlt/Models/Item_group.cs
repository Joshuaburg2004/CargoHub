
using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]
public class Item_group: Base
{
    public Guid Id {get; set;}
    public string Name { get; set; }

    public string Description {get; set;}
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Item_group(Guid id, string name, string Description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = Description;
    }

}