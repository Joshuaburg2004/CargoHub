public class EndpointAccesses
{
    public AccessLevel Clients;
    public AccessLevel Inventories;
    public AccessLevel Items;
    public AccessLevel ItemTypes;
    public AccessLevel ItemGroups;
    public AccessLevel ItemLines;
    public AccessLevel Locations;
    public AccessLevel Orders;
    public AccessLevel Shipments;
    public AccessLevel Suppliers;
    public AccessLevel Transfers;
    public AccessLevel Warehouses;
    public AccessLevel Backup;

    public EndpointAccesses(AccessLevel Clients, AccessLevel Inventories, AccessLevel Items, AccessLevel ItemTypes, AccessLevel ItemGroups, AccessLevel ItemLines, AccessLevel Locations, AccessLevel Orders, AccessLevel Shipments, AccessLevel Suppliers, AccessLevel Transfers, AccessLevel Warehouses, AccessLevel Backup)
    {
        this.Clients = Clients;
        this.Inventories = Inventories;
        this.Items = Items;
        this.ItemTypes = ItemTypes;
        this.ItemGroups = ItemGroups;
        this.ItemLines = ItemLines;
        this.Locations = Locations;
        this.Orders = Orders;
        this.Shipments = Shipments;
        this.Suppliers = Suppliers;
        this.Transfers = Transfers;
        this.Warehouses = Warehouses;
        this.Backup = Backup;
    }

}