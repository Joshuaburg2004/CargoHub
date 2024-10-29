using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]

public class Location : Base
{
    public int Id { get; set; }
    public int warehouse_Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CreatedAt { get; set; } = GetTimeStamp();
    public string UpdatedAt { get; set; } = GetTimeStamp();

    public Location(int id, int warehouse_id, string code, string name)
    {
        Id = id;
        warehouse_Id = warehouse_id;
        Code = code;
        Name = name;
    }
}