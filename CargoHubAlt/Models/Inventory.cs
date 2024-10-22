public class Inventory : Base
{
    public Guid Id { get; set; }
    public string Item_id { get; set; }
    public string Description { get; set; }
    public string Item_reference { get; set; }
    public List<int> Locations { get; set; }
    public int Total_on_hand { get; set; }
    public int Total_expected { get; set; }
    public int Total_ordered { get; set; }
    public int Total_allocated { get; set; }
    public int Total_available { get; set; }
    public string Created_at { get; set; } = GetTimeStamp();
    public string Updated_at { get; set; } = GetTimeStamp();

    public Inventory(string item_id, string description, string item_reference, List<int> locations, int total_on_hand, int total_expected, int total_ordered, int total_allocated, int total_available)
    {
        Id = Guid.NewGuid();
        Item_id = item_id;
        Description = description;
        Item_reference = item_reference;
        Locations = locations;
        Total_on_hand = total_on_hand;
        Total_expected = total_expected;
        Total_ordered = total_ordered;
        Total_allocated = total_allocated;
        Total_available = total_available;
    }
}
