using AquaFlaim.CommonAPI;
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
    }
}
