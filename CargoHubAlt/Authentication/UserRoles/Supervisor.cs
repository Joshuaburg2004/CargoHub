public class SuperVisor: User
{
    public SuperVisor(string ApiKey, string App, int WarehouseID): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, false, false, false),
        Inventories: new AccessLevel(Access.Own, true, false, false, false),
        Items: new AccessLevel(Access.Own, true, false, false ,false),
        ItemTypes: new AccessLevel(Access.False, false, false, false, false),
        ItemGroups: new AccessLevel(Access.False, false, false, false, false),
        ItemLines: new AccessLevel(Access.False, false, false, false, false),
        Locations: new AccessLevel(Access.Own, true, false, false, false),
        Orders: new AccessLevel(Access.Own, true, false, false, false),
        Shipments: new AccessLevel(Access.True, true, false, false, false),
        Suppliers: new AccessLevel(Access.False, true, false, false, false),
        Transfers: new AccessLevel(Access.True, true, true, true, false),
        Warehouses: new AccessLevel(Access.False, true, false, false, false),
        Backup: new AccessLevel(Access.False, false, false, false, false)
    ))
    {
        this.WarehouseID = WarehouseID;
    }
}