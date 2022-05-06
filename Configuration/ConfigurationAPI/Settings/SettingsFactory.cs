using AquaFlaim.CommonAPI;

namespace ConfigurationAPI
{
    public class SettingsFactory : ISettingsFactory
    {
        public AquaFlaim.CommonCore.ISettings CreateCore(Settings settings)
        {
            return new CoreSettings()
            {
                ConnectionString = settings.ConnectionString,
                EnableDatabaseAccessToken = settings.EnableDatabaseAccessToken
            };
        }

        public AquaFlaim.Interface.Log.ISettings CreateLog(Settings settings, string token)
        {
            return new LogSettings(settings.LogApiBaseAddress, token);
        }
    }
}
