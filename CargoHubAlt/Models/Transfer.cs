using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
using CargoHubAlt.Database;

namespace CargoHubAlt.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public int? TransferFrom { get; set; } = 0;
        public int? TransferTo { get; set; } = 0;
        public string? TransferStatus { get; set; }
        public string? CreatedAt { get; set; } = Base.GetTimeStamp();
        public string? UpdatedAt { get; set; } = Base.GetTimeStamp();
        public List<TransferItem> Items { get; set; } = new List<TransferItem>();
        public Transfer(){ }
        public Transfer(int id, string reference, int transfer_from, int transfer_to, string transfer_status, List<TransferItem> items)
        {
            Id = id;
            Reference = reference;
            TransferFrom = transfer_from;
            TransferTo = transfer_to;
            TransferStatus = transfer_status;
            Items = items;
        }
    }
    [Owned]
    public class TransferItem{
        public string? ItemId { get; set; }
        public int Amount {get; set;}
    }

}
