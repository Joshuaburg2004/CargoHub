public class WarehouseManager: User
{
    public WarehouseManager(string ApiKey, string App): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, true, true, false),
        Inventories: new AccessLevel(Access.True, true, true, true, true),
        Items: new AccessLevel(Access.True, true, true, true, false),
        ItemTypes: new AccessLevel(Access.True, true, true, true, false),
        ItemGroups: new AccessLevel(Access.True, true, true, true, false),
        ItemLines: new AccessLevel(Access.True, true, true, true, false),
        Locations: new AccessLevel(Access.True, true, true, true, true),
        Orders: new AccessLevel(Access.True, true, true, true, true),
        Shipments: new AccessLevel(Access.True, true, true, true, true),
        Suppliers: new AccessLevel(Access.True, true, true, true, true),
        Transfers: new AccessLevel(Access.True, true, true, true, true),
        Warehouses: new AccessLevel(Access.True, true, true, false, false),
        Backup: new AccessLevel(Access.False, false, false, false, false)
    ))
    {}
}