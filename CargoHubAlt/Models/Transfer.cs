using Microsoft.AspNetCore.Http.Features;

public class Transfer
{
    public Guid Id { get; set; }
    public string Reference { get; set; }
    public string Transfer_from { get; set; }
    public string Transfer_to { get; set; }
    public string Transfer_status { get; set; }
    public string Created_at { get; set; }
    public string Updated_at { get; set; }
    public List<Items> Items { get; set; }
}
