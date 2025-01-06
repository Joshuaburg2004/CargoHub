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
                return (true, "Backup created successfully at: " + backupFolderPath);
            }
            catch (Exception ex)
            {
                return (false, $"Error during backup: {ex}");
            }
        }

        private async Task<(bool, string)> CreateBackupDatabase(string backupFolderPath)
        {
            string source = "./CargoHubDatabase.db";

            try
            {
                if (!File.Exists(source))
                {
                    return (false, "No database found to backup");
                }
                string destinationFilePath = Path.Combine(backupFolderPath, "CargoHubDatabaseBackup.db");
                File.Copy(source, destinationFilePath);
                return (true, $"Backup saved at: {destinationFilePath}");
            }
            catch (Exception ex)
            {
                return (false, $"Error during database backup: {ex}");
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
                return (true, $"Logs backup saved at: {destinationFilePath}");
            }
            catch (Exception ex)
            {
                return (false, $"Error during logs backup: {ex}");
            }
        }
        public async Task<(bool,string)> Uploadbackup(string backupFolderPath)
        {
            try
            {
                string folderfound = Path.Combine(this.backupRoot, backupFolderPath);
                if (!Directory.Exists(folderfound))
                    return (false, "folder does not exist");
                string backuplogsfolder = Path.Combine(folderfound, "LogsBackup");
                if (!Directory.Exists(backuplogsfolder))
                {
                    (bool success, string fail) = await this.uploadBackupLogs(backuplogsfolder);
                }

                return (true, "nothing happened");
            }
            catch (Exception ex)
            {
                return (false, $"Error during logs backup: {ex}");
            }
        }


        public async Task<(bool,string)> uploadBackupLogs(string backupFolderPath)
        {
            string baseLogsFolder = "./Logs";
            string errorMessages = "";


            if (!Directory.Exists(backupFolderPath))
                return (false, "the back up folder searched for has no logsbackup");
            bool clientlogsSuccess = false;
            string clientlogname = "ClientController.log";

            if (Path.Exists(Path.Combine(backupFolderPath, clientlogname)))
            {
                File.Copy(Path.Combine(backupFolderPath, clientlogname), Path.Combine(baseLogsFolder, clientlogname));
                clientlogsSuccess = true;
            }
            else 
            {
                errorMessages += "the Client log folder does not exist;";
            }

            bool itemLogSuccess = false;
            string itemLogName = "ItemController.log";

            if (Path.Exists(Path.Combine(backupFolderPath, itemLogName)))
            {
                File.Copy(Path.Combine(backupFolderPath, itemLogName), Path.Combine(baseLogsFolder, itemLogName));
                itemLogSuccess = true;
            }
            else 
            {
                errorMessages += "the Item log folder does not exist;";
            }

            bool OrderLogSuccess = false;
            string OrderLogName = "OrderController.log";

            if (Path.Exists(Path.Combine(backupFolderPath, OrderLogName)))
            {
                File.Copy(Path.Combine(backupFolderPath, OrderLogName), Path.Combine(baseLogsFolder, OrderLogName));
                OrderLogSuccess = true;
            }
            else 
            {
                errorMessages += "the Order log folder does not exist;";
            }

            bool ShipmentLogSuccess = false;
            string ShipmentLogName = "ShipmentController.log";

            if (Path.Exists(Path.Combine(backupFolderPath, ShipmentLogName)))
            {
                File.Copy(Path.Combine(backupFolderPath, ShipmentLogName), Path.Combine(baseLogsFolder, ShipmentLogName));
                ShipmentLogSuccess = true;
            }
            else 
            {
                errorMessages += "the Shipment log folder does not exist;";
            }

            if (clientlogsSuccess && itemLogSuccess && OrderLogSuccess && ShipmentLogSuccess)
            {
                return (true, errorMessages);
            }

            return (false, errorMessages);

        }

    }


}