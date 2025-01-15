using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;

namespace CargoHubAlt.Services.ServicesV2{
    public class AnalyticsService : IAnalyticsService{
        private readonly CargoHubContext _context;
        public AnalyticsService(CargoHubContext context){
            _context = context;
        }
        public async Task<Dictionary<string, Dictionary<int, Item>>> GetAnalytics(DateOnly? FromDate = null, DateOnly? ToDate = null){
            if (FromDate == null){
                FromDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            }
            if (ToDate == null){
                ToDate = DateOnly.FromDateTime(DateTime.Now);
            }
            List<Order> orders = await _context.Orders.Where(o => DateOnly.Parse(o.OrderDate.Replace("T", " ").Remove()) <= ToDate && )
        }
    }
}