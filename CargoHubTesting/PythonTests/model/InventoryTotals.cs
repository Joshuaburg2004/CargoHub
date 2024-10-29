namespace PythonTests.models;

public class InventoryTotals
{
    public int total_expected { get; set; }
    public int total_ordered { get; set; }
    public int total_allocated { get; set; }
    public int total_available { get; set; }

    public InventoryTotals(int total_expected, int total_ordered, int total_allocated, int total_available)
    {
        this.total_expected = total_expected;
        this.total_ordered = total_ordered;
        this.total_allocated = total_allocated;
        this.total_available = total_available;
    }
}