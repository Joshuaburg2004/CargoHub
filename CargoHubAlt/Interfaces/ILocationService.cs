public interface ILocationService
{
    Task<List<Location>> GetAllLocations();
    Task<Location?> GetLocation(Guid id);
    Task<IEnumerable<Location?>> GetBatchLocation(IEnumerable<Guid> Ids);
    Task<Guid> AddLocation(Location Location);
    Task<Location?> UpdateLocation(Guid id, Location Location);
    Task<Location?> DeleteLocation(Guid id);
}