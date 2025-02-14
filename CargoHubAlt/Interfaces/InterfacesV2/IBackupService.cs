using CargoHubAlt.Models;

namespace CargoHubAlt.Interfaces.InterfacesV2
{
    public interface IBackupService
    {
        public Task<(bool, string)> CreateBackup();
        public Task<(bool,string)> UploadBackupFull(string toUpload);
    }
}