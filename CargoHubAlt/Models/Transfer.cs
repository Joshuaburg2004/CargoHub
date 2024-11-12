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
        public int Transfer_From { get; set; }
        public int Transfer_To { get; set; }
        public string? Transfer_Status { get; set; }
        public string? Created_At { get; set; }
        public string? Updated_At { get; set; }
        public List<TransferItem> Items { get; set; } = new List<TransferItem>();
        public Transfer(){ }
        public Transfer(int id, string reference, int transfer_From, int transfer_To, string transfer_Status, string created_At, string updated_At, List<TransferItem> items)
        {
            Id = id;
            Reference = reference;
            Transfer_From = transfer_From;
            Transfer_To = transfer_To;
            Transfer_Status = transfer_Status;
            Created_At = created_At;
            Updated_At = updated_At;
            Items = items;
        }
    }
    public class TransferItem{
        public int TransferId { get; set;}
        public string? Item_Id { get; set; }
        public int Amount {get; set;}
    }

}
