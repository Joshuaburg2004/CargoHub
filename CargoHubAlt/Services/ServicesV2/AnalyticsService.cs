using CargoHubAlt.Database;
using CargoHubAlt.Interfaces.InterfacesV2;
using CargoHubAlt.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHubAlt.Services.ServicesV2{
    public class AnalyticsService : IAnalyticsService{
        private readonly CargoHubContext _context;
        public AnalyticsService(CargoHubContext context){
            _context = context;
        }
        public async Task<Analytic> GetAnalytics(DateOnly? FromDate = null, DateOnly? ToDate = null){
            if (FromDate == null){
                FromDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            }
            if (ToDate == null){
                ToDate = DateOnly.FromDateTime(DateTime.Now);
            }
            List<Order> orders = await _context.Orders.ToListAsync();
            List<Order> filteredOrders = new List<Order>();
            orders.ForEach(o => {
                if(DateOnly.TryParse(o.OrderDate, out var date)){
                    if(date >= FromDate && date <= ToDate){
                        filteredOrders.Add(o);
                    }
                }
            });
            List<List<OrderedItem>> orderedItemsLists = orders.Select(o => o.Items).ToList();
            List<OrderedItem> orderedItemsTotals = new List<OrderedItem>();
            foreach(List<OrderedItem> orderedItems in orderedItemsLists){
                orderedItems.ForEach(o => {
                    if(!orderedItemsTotals.Where(i => i.ItemId == o.ItemId).Any()){
                        orderedItemsTotals.Add(o);
                    }
                    else{
                        orderedItemsTotals.ForEach(i => {
                            if(i.ItemId == o.ItemId){
                                i.Amount += o.Amount;
                                return;
                            }
                        });
                    }
                });
            }
            List<OrderedItem> bestItems = orderedItemsTotals.OrderByDescending(o => o.Amount).Take(10).ToList();
            Dictionary<int, Item> BestPerformers = new Dictionary<int, Item>();
            int n = 1;
            bestItems.ForEach(i => {
                BestPerformers.Add(n, _context.Items.Where(p => p.Uid == i.ItemId).First());
                n++;
            });
            List<OrderedItem> worstItems = orderedItemsTotals.OrderBy(o => o.Amount).Take(10).ToList();
            Dictionary<int, Item> WorstPerformers = new Dictionary<int, Item>();
            int m = 1;
            worstItems.ForEach(i => {
                WorstPerformers.Add(m, _context.Items.Where(p => p.Uid == i.ItemId).First());
                m++;
            });
            return new Analytic(){
                FromDate = (DateOnly)FromDate,
                ToDate = (DateOnly)ToDate,
                BestPerformers = BestPerformers,
                WorstPerformers = WorstPerformers
            };
        }
    }
}