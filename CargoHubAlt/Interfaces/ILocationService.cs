using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces
{
    public interface ILocationService
    {
        public Task<Location?> GetOneLocation(int id);
        public Task<IEnumerable<Location>> GetAllLocations();
        public Task<int?> AddLocation(Location toAdd);
        public Task<Location?> UpdateLocation(int id, Location toUpdate);
        public Task<Location?> DeleteLocation(int id);
    }
}
