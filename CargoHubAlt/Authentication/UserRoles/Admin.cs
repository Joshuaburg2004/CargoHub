public class Admin: User
{
    public Admin(string ApiKey, string App): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, true, true, true),
        Inventories: new AccessLevel(Access.True, true, true, true, true),
        Items: new AccessLevel(Access.True, true, true, true, true),
        ItemTypes: new AccessLevel(Access.True, true, true, true, true),
        ItemGroups: new AccessLevel(Access.True, true, true, true, true),
        ItemLines: new AccessLevel(Access.True, true, true, true, true),
        Locations: new AccessLevel(Access.True, true, true, true, true),
        Orders: new AccessLevel(Access.True, true, true, true, true),
        Shipments: new AccessLevel(Access.True, true, true, true, true),
        Suppliers: new AccessLevel(Access.True, true, true, true, true),
        Transfers: new AccessLevel(Access.True, true, true, true, true),
        Warehouses: new AccessLevel(Access.True, true, true, true, true),
        Backup: new AccessLevel(Access.True, true, true, true, true)
    ))
    {
    }
}