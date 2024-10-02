using System.Globalization;

public class Base{
    public string GetTimeStamp(){
        return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
    }
}