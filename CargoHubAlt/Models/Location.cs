using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]

public class Location : Base
{
    public Guid Id { get; set; }
    public Guid warehouse_Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Location(string code, string name)
    {
        Id = new Guid();
        warehouse_Id = new Guid();
        Code = code;
        Name = name;
    }
}