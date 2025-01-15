using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoHubAlt.Models
{
    public class PickingOrder : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int WarehouseId { get; set; }
        public List<Location> Route { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string CreatedAt { get; set; } = GetTimeStamp();
        public string UpdatedAt { get; set; } = GetTimeStamp();

        public PickingOrder() { }
        public PickingOrder(int orderId, int warehouseId, List<Location> route)
        {
            OrderId = orderId;
            WarehouseId = warehouseId;
            Route = route;
        }
    }

}