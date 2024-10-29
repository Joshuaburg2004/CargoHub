public interface ILocationService
{
    Task<List<Location>> GetAllLocations();
    Task<Location?> GetOneLocation(int id);
    Task<int?> AddLocation(Location Location);
    Task<Location?> UpdateLocation(int id, Location Location);
    Task<Location?> DeleteLocation(int id);
}