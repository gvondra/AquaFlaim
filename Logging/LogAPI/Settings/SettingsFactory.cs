namespace LogAPI
{
    public class SettingsFactory : ISettingsFactory
    {
        public AquaFlaim.CommonCore.ISettings CreateCore(Settings settings)
        {
            return new AquaFlaim.CommonAPI.CoreSettings()
            {
                ConnectionString = settings.ConnectionString,
                ConnectionStringUser = settings.ConnectionStringUser,
                EnableDatabaseAccessToken = settings.EnableDatabaseAccessToken,
                KeyVaultAddress = settings.KeyVaultAddress
            };
        }

        public BrassLoon.DataClient.ISqlSettings CreateData(Settings settings)
        {
            return new AquaFlaim.CommonCore.DataSettings(
                CreateCore(settings)
                );
        }

        public AquaFlaim.Interface.Log.ISettings CreateLog(Settings settings, string token)
        {
            return new LogApiSettings
            {
                BaseAddress = settings.LogApiBaseAddress,
                Token = token
            };
        }
    }
}
