public record Inventory{
    public int Id {get; set;}
    public int ItemId {get; set;}
    public string Description {get; set;}
    public string ItemReference {get; set;}
    // uses ID, not LocationObject.
    public List<int> Locations {get; set;}
    public int TotalOnHand {get; set;}
    public int TotalExpected {get; set;}
    public int TotalOrdered {get; set;}
    public int TotalAllocated {get; set;}
    public int TotalAvailable {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public Inventory(int id, int itemId, string description, string itemReference, List<int> locations, int totalOnHand, int totalExpected, int totalOrdered, int totalAllocated, int totalAvailable){
        Id = id;
        Description = description;
        
}