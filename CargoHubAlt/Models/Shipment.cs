public class Shipment
{
    public Guid Id { get; set; }
    public Guid Order_id { get; set; } // maybe this should be a list of order ids
    public Guid Source_id { get; set; }
    public string Order_date { get; set; }
    public string Request_date { get; set; }
    public string Shipment_date { get; set; }
    public string Shipment_type { get; set; }
    public string Shipment_status { get; set; }
    public string Notes { get; set; }
    public string Carrier_code { get; set; }
    public string Carrier_description { get; set; }
    public string Service_code { get; set; }
    public string Payment_type { get; set; }
    public string Transfer_mode { get; set; }
    public int Total_package_count { get; set; }
    public int Total_package_weight { get; set; }
    public string Created_at { get; set; }
    public string Updated_at { get; set; }
    public List<Item> Items { get; set; }
}