using System.Reflection;
using System.Text.Json;

public static class ApiKeyOptions
{
    public static List<User> Users { get; set; } = new List<User>();
    static ApiKeyOptions()
    {
        Users = ReadJsonUsers();
    }
    public static void WriteJsonUsers(List<User> users)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(users, new Newtonsoft.Json.JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented

        });
        File.WriteAllText(Path.Join(Directory.GetCurrentDirectory(), "Authentication/users.json"), json);
    }
    private static List<User> ReadJsonUsers()
    {
        var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Authentication", "users.json");
        var json = File.ReadAllText(FilePath);
        var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
        return users ?? new List<User>();
    }
}