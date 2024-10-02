using System.Text.Json;
public class Clients : Base{
    static List<Client> clients = [];
    public string DataPath;
    public Clients(string rootPath, bool isDebug){
        DataPath = rootPath + "clients.json";
        Load(isDebug);
    }
    
    private void Load(bool isDebug){
        if(isDebug){
            return;
        }
        clients = JsonSerializer.Deserialize<List<Client>>(File.ReadAllText(DataPath))!;
    }
}