public class Location: Base{
    

    public Guid Id {get; set;}
    public Guid warehouse_Id {get; set;}
    public string Code {get; set;}
    public string Name {get; set;}
    public string created_at {get; set;}
    public string updated_at {get; set;}

    public Location(Guid id, Guid warehouse_id, string code, string name, string created_at, string updated_at)
    {
        this.Id = id;
        this.warehouse_Id = warehouse_Id;
        this.Code = code;
        this.Name = name;
        this.created_at = created_at;
        this.updated_at = updated_at;
    }

}