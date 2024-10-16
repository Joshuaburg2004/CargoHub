public class ItemGroupService: IItemGroupService
{
    private readonly CargoHubContext _cargoHubContext;
    public ItemGroupService(CargoHubContext context){
        _cargoHubContext = context;
    }



}