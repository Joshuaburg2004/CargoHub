public class Shipment
{
    public Guid id { get; set; }
    public Guid order_id { get; set; } // maybe this should be a list of order ids
    public Guid source_id { get; set; }
    public string order_date { get; set; }
    public string request_date { get; set; }
    public string shipment_date { get; set; }
    public string shipment_type { get; set; }
    public string shipment_status { get; set; }
    public string notes { get; set; }
    public string carrier_code { get; set; }
    public string carrier_description { get; set; }
    public string service_code { get; set; }
    public string payment_type { get; set; }
    public string transfer_mode { get; set; }
    public int total_package_count { get; set; }
    public double total_package_weight { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public List<Item> items { get; set; }
}