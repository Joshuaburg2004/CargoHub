using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface ILocationServiceV2
    {
        public Task<Location?> GetOneLocation(int id);
        public Task<List<Location>> GetAllLocations();
        public Task<List<Location>> GetAllLocations(int? pageIndex);
        public Task<int?> AddLocation(Location toAdd);
        public Task<Location?> UpdateLocation(int id, Location toUpdate);
        public Task<Location?> DeleteLocation(int id);
        public Task DisperseAllInventoriesOverLocations();

        public Task LoadFromJson(string path);
    }
}
