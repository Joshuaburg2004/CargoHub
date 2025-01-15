namespace CargoHubAlt.Models {
    public class Analytic{
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set;}
        public Dictionary<int, Item> BestPerformers { get; set; } = new Dictionary<int, Item>();
        public Dictionary<int, Item> WorstPerformers { get; set; } = new Dictionary<int, Item>();
    }
}