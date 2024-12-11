public class Analyst: User
{
    public Analyst(string ApiKey, string App): base(ApiKey, App,
    new EndpointAccesses(
        Clients: new AccessLevel(Access.True, true, false, false, false),
        Inventories: new AccessLevel(Access.True, true, false, false, false),
        Items: new AccessLevel(Access.True, true, false, false ,false),
        ItemTypes: new AccessLevel(Access.True, true, false, false, false),
        ItemGroups: new AccessLevel(Access.True, true, false, false, false),
        ItemLines: new AccessLevel(Access.True, true, false, false, false),
        Locations: new AccessLevel(Access.True, true, false, false, false),
        Orders: new AccessLevel(Access.True, true, false, false, false),
        Shipments: new AccessLevel(Access.True, true, false, false, false),
        Suppliers: new AccessLevel(Access.True, true, false, false, false),
        Transfers: new AccessLevel(Access.True, true, false, false, false),
        Warehouses: new AccessLevel(Access.True, true, false, false, false)
    ))
    {}
}