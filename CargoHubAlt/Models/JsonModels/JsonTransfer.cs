using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CargoHubAlt.Models;
namespace CargoHubAlt.JsonModels{
    public class JsonTransfer : JsonBase{
        public int id { get; set; }
        public string? reference { get; set; }
        public int transfer_from { get; set; } = 0;
        public int transfer_to { get; set; } = 0;
        public string? transfer_status { get; set; }
        public string created_at { get; set; } = GetTimeStamp();
        public string updated_at { get; set; } = GetTimeStamp();
        public List<JsonTransferItem> items { get; set; } = new List<JsonTransferItem>();
        public Transfer ToTransfer(){
            return new Transfer(){
                Id = id,
                Reference = reference,
                TransferFrom = transfer_from,
                TransferTo = transfer_to,
                TransferStatus = transfer_status,
                CreatedAt = created_at,
                UpdatedAt = updated_at
            };
        }
    }
    public class JsonTransferItem{
        public string? item_id { get; set; }
        public int amount { get; set; }
        public TransferItem ToTransferItem(){
            return new TransferItem(){
                ItemId = item_id,
                Amount = amount
            };
        }
    }
}