public class Logistics: User
{
    public Logistics(string ApiKey, string App): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, true, true, false),
        Inventories: new AccessLevel(Access.True, true, true, true, false),
        Items: new AccessLevel(Access.True, true, true, false ,false),
        ItemTypes: new AccessLevel(Access.True, true, false, false, false),
        ItemGroups: new AccessLevel(Access.True, true, false, false, false),
        ItemLines: new AccessLevel(Access.True, true, false, false, false),
        Locations: new AccessLevel(Access.True, true, false, false, false),
        Orders: new AccessLevel(Access.True, true, true, true, false),
        Shipments: new AccessLevel(Access.True, true, true, true, false),
        Suppliers: new AccessLevel(Access.True, true, true, true, false),
        Transfers: new AccessLevel(Access.False, false, false, false, false),
        Warehouses: new AccessLevel(Access.True, true, false, false, false),
        Backup: new AccessLevel(Access.False, false, false, false, false)
    ))
    {}
}