public class InventoryManager: User
{
    public InventoryManager(string ApiKey, string App, int WarehouseID): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, false, false, false),
        Inventories: new AccessLevel(Access.True, true, true, true, true),
        Items: new AccessLevel(Access.True, true, false, false ,false),
        ItemTypes: new AccessLevel(Access.True, true, true, true, false),
        ItemGroups: new AccessLevel(Access.True, true, true, true, false),
        ItemLines: new AccessLevel(Access.True, true, true, true, false),
        Locations: new AccessLevel(Access.Own, true, true, true, true),
        Orders: new AccessLevel(Access.True, true, false, false, false),
        Shipments: new AccessLevel(Access.True, true, false, false, false),
        Suppliers: new AccessLevel(Access.True, true, false, false, false),
        Transfers: new AccessLevel(Access.True, true, true, true, true),
        Warehouses: new AccessLevel(Access.False, true, false, false, false)
    ))
    {
        this.WarehouseID = WarehouseID;
    }
}