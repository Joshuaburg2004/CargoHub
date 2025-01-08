using CargoHubAlt.Interfaces.InterfacesV2;

namespace CargoHubAlt.Services.ServicesV2
{
    public class BackupService : IBackupService
    {
        public BackupService() { }

        public async Task<(bool, string)> CreateBackup()
        {
            string backupRoot = $"./Backups";

            try
            {
                // If directory doesn't exist, create it
                if (!Directory.Exists(backupRoot))
                {
                    Directory.CreateDirectory(backupRoot);
                }

                // Create a new folder for the backup
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss");
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
    }
}