using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2{
    public interface IAnalyticsService{
        public Task<Dictionary<string, Dictionary<int, Item>>> GetAnalytics(DateOnly? FromDate = null, DateOnly? ToDate = null);
    }
}