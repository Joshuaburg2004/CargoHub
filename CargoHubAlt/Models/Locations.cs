using Microsoft.EntityFrameworkCore;
[PrimaryKey("Id")]
public class Location : Base
{
    public Guid Id { get; set; }
    public Guid warehouse_Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }

    public Location(string code, string name, string created_at, string updated_at)
    {
        Id = new Guid();
        warehouse_Id = new Guid();
        Code = code;
        Name = name;
        this.created_at = created_at;
        this.updated_at = updated_at;
    }
}