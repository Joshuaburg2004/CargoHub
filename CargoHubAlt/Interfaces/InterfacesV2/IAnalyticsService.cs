using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2{
    public interface IAnalyticsService{
        public Task<Analytic> GetAnalytics(DateOnly? FromDate = null, DateOnly? ToDate = null);
    }
}