using AquaFlaim.CommonAPI;

namespace FormsAPI
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

        public AquaFlaim.Interface.Log.ISettings CreateLog(Settings settings, string token)
        {
            return new LogSettings(settings.LogApiBaseAddress, token);
        }
    }
}
