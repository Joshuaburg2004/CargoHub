namespace CargoHubAlt.Models
{
    public class Base
    {
        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "Z";
        }
    }
}
