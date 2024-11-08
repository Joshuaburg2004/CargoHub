using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]
public class Location : Base
{
    public int Id { get; set; }
    public int Warehouse_Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Created_At { get; set; } = GetTimeStamp();
    public string Updated_At { get; set; } = GetTimeStamp();

    public Location(int id, int warehouse_Id, string code, string name)
    {
        Id = id;
        Warehouse_Id = warehouse_Id;
        Code = code;
        Name = name;
    }
}