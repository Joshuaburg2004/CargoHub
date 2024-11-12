using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    public class Transfer
    {
        public int id { get; set; }
        public string? reference { get; set; }
        public int? transfer_from { get; set; } = 0;
        public int? transfer_to { get; set; } = 0;
        public string? transfer_status { get; set; }
        public string? created_at { get; set; } = Base.GetTimeStamp();
        public string? updated_at { get; set; } = Base.GetTimeStamp();
        public List<TransferItem> items { get; set; } = new List<TransferItem>();
        public Transfer(){ }
        public Transfer(int id, string reference, int transfer_from, int transfer_to, string transfer_status, List<TransferItem> items)
        {
            this.id = id;
            this.reference = reference;
            this.transfer_from = transfer_from;
            this.transfer_to = transfer_to;
            this.transfer_status = transfer_status;
            this.items = items;
        }
    }
    [Owned]
    public class TransferItem{
        public string? item_id { get; set; }
        public int amount {get; set;}
    }

}
