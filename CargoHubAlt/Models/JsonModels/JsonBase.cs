using CargoHubAlt.Database;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CargoHubAlt.JsonModels{
    public class JsonBase{
        public static string GetTimeStamp(){
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}