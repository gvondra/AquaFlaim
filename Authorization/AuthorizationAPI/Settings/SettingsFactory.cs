using AquaFlaim.CommonAPI;
using AquaFlaim.Interface.Log;

namespace AuthorizationAPI
{
    public class SettingsFactory : ISettingsFactory
    {
        public AquaFlaim.CommonCore.ISettings CreateCore(Settings settings)
        {
            return new CoreSettings()
            {
                ConnectionString = settings.ConnectionString,
                EnableDatabaseAccessToken = settings.EnableDatabaseAccessToken,
                ConnectionStringUser = settings.ConnectionStringUser,
                KeyVaultAddress = settings.KeyVaultAddress                
            };
        }

        public ISettings CreateLog(Settings settings, string token)
        {
            return new LogSettings(settings.LogApiBaseAddress, token);
        }
    }
}
