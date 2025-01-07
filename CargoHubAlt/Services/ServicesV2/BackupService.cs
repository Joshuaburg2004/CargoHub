using CargoHubAlt.Interfaces.InterfacesV2;

namespace CargoHubAlt.Services.ServicesV2
{
    public class BackupService : IBackupService
    {
        public BackupService() { }

        private string backupRoot = $"./Backups";

        public async Task<(bool, string)> CreateBackup()
        {

            try
            {
                // If directory doesn't exist, create it
                if (!Directory.Exists(backupRoot))
                {
                    Directory.CreateDirectory(backupRoot);
                }

                // Create a new folder for the backup
                string timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH-mm-ss");
                string backupFolderPath = Path.Combine(backupRoot, timestamp);
                Directory.CreateDirectory(backupFolderPath);

                // Create the backup files
                await CreateBackupDatabase(backupFolderPath);
                await CreateBackupLogs(backupFolderPath);
                return await Task.FromResult((true, "Backup created successfully at: " + backupFolderPath));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, $"Error during backup: {ex}"));
            }
        }

        private async Task<(bool, string)> CreateBackupDatabase(string backupFolderPath)
        {
            string source = "./CargoHubDatabase.db";

            try
            {
                if (!File.Exists(source))
                {
                    return await Task.FromResult((false, "No database found to backup"));
                }
                string destinationFilePath = Path.Combine(backupFolderPath, "CargoHubDatabaseBackup.db");
                File.Copy(source, destinationFilePath);
                return await Task.FromResult((true, $"Backup saved at: {destinationFilePath}"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, $"Error during database backup: {ex}"));
            }
        }

        private async Task<(bool, string)> CreateBackupLogs(string backupFolderPath)
        {
            string source = "./Logs";

            try
            {
                if (!Directory.Exists(source))
                {
                    return (false, "No logs found to backup");
                }
                string destinationFilePath = Path.Combine(backupFolderPath, "LogsBackup");
                Directory.CreateDirectory(destinationFilePath);
                foreach (string file in Directory.GetFiles(source))
                {
                    File.Copy(file, Path.Combine(destinationFilePath, Path.GetFileName(file)));
                }
                return await Task.FromResult((true, $"Logs backup saved at: {destinationFilePath}"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, $"Error during logs backup: {ex}"));
            }
        }
        public async Task<(bool,string)> UploadBackupFull(string backupFolderPath)
        {
            try
            {
                string folderfound = Path.Combine(this.backupRoot, backupFolderPath);
                if (!Directory.Exists(folderfound))
                    return (false, "backup folder does not exist");

                // string backuplogsfolder = Path.Combine(folderfound, "LogsBackup");
                // (bool success, string fail) logsuccess = (false, "logs folder inside backupdatabase not found;" );
                // if (Directory.Exists(backuplogsfolder))
                // {
                //     logsuccess = await this.uploadBackupLogs(backuplogsfolder);
                // }
                // else return (false, "no backuplogs found");

                string backupdatabase = Path.Combine(folderfound, "CargoHubDatabaseBackup.db");
                (bool success, string fail) databasesuccess = (false, "backup database file not found");
                if (File.Exists(backupdatabase))
                {
                    databasesuccess = uploadBackupDatabase(folderfound);
                }

                if (databasesuccess.success == true)
                {
                    return (true, "successfully uploaded the backup");
                } 
                return (false, databasesuccess.fail);
            }
            catch (Exception ex)
            {
                return (false, $"Error during logs backup: {ex}");
            }
        }


        private (bool, string) uploadBackupDatabase(string backupFolderPath)
        {

            string errorMessages = "";
            if (!Directory.Exists(backupFolderPath))
                return (false, $"the back up folder searched for has no database, searched for {backupFolderPath}");
            string databaseBackupString = "CargoHubDatabaseBackup.db";
            string databaseName = "CargoHubDatabase.db";

            if (File.Exists(Path.Combine(backupFolderPath, databaseBackupString)))
            {
                if (File.Exists($"./{databaseName}"))
                {
                    File.Delete($"./{databaseName}");
                }
                File.Copy(Path.Combine(backupFolderPath, databaseBackupString), $"./{databaseName}");
                return (true, errorMessages);
            }
            
            return (false, "backup folder database does not exist;");

        }



        private async Task<(bool,string)> uploadBackupLogs(string backupFolderPath)
        {
            string baseLogsFolder = "./Logs";
            string errorMessages = "";


            if (!Directory.Exists(backupFolderPath))
                return (false, $"the back up folder searched for has no logsbackup, searched for {backupFolderPath}");
            bool clientlogsSuccess = false;
            string clientLogname = "ClientController.log";
            string ClientBaseLogs = Path.Combine(baseLogsFolder, clientLogname);
            string clientBackupLogs = Path.Combine(backupFolderPath, clientLogname);

            if (Path.Exists(clientBackupLogs))
            {
                await File.WriteAllLinesAsync(ClientBaseLogs, await File.ReadAllLinesAsync(clientBackupLogs)); 
                clientlogsSuccess = true;
            }
            else 
            {
                errorMessages += "the Client log folder does not exist;";
            }

            bool ItemlogsSuccess = false;
            string ItemLogname = "ItemController.log";
            string ItemBaseLogs = Path.Combine(baseLogsFolder, ItemLogname);
            string ItemBackupLogs = Path.Combine(backupFolderPath, ItemLogname);

            if (Path.Exists(ItemBackupLogs))
            {
                await File.WriteAllLinesAsync(ItemBaseLogs, await File.ReadAllLinesAsync(ItemBackupLogs)); 
                ItemlogsSuccess = true;
            }
            else 
            {
                errorMessages += "the Item log folder does not exist;";
            }


            bool OrderlogsSuccess = false;
            string OrderLogname = "OrderController.log";
            string OrderBaseLogs = Path.Combine(baseLogsFolder, OrderLogname);
            string OrderBackupLogs = Path.Combine(backupFolderPath, OrderLogname);

            if (Path.Exists(OrderBackupLogs))
            {
                await File.WriteAllLinesAsync(OrderBaseLogs, await File.ReadAllLinesAsync(OrderBackupLogs)); 
                OrderlogsSuccess = true;
            }
            else 
            {
                errorMessages += "the Order log folder does not exist;";
            }

            bool ShipmentLogSuccess = false;
            string ShipmentLogname = "ShipmentController.log";
            string ShipmentBaseLogs = Path.Combine(baseLogsFolder, ShipmentLogname);
            string ShipmentBackupLogs = Path.Combine(backupFolderPath, ShipmentLogname);

            if (Path.Exists(ShipmentBackupLogs))
            {
                await File.WriteAllLinesAsync(ShipmentBaseLogs, await File.ReadAllLinesAsync(ShipmentBackupLogs)); 
                ShipmentLogSuccess = true;
            }
            else 
            {
                errorMessages += "the Shipment log folder does not exist;";
            }

            if (clientlogsSuccess && ItemlogsSuccess && OrderlogsSuccess && ShipmentLogSuccess)
            {
                return (true, errorMessages);
            }

            return (false, errorMessages);

        }

    }


}