using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV1
{
    public interface ILocationServiceV1
    {
        public Task<Location?> GetOneLocation(int id);
        public Task<List<Location>> GetAllLocations();
        public Task<int?> AddLocation(Location toAdd);
        public Task<Location?> UpdateLocation(int id, Location toUpdate);
        public Task<Location?> DeleteLocation(int id);
        public Task LoadFromJson(string path);
    }
}
