namespace ConfigurationAPI
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public bool EnableDatabaseAccessToken { get; set; } = false;
        public string InternalIdIssuer { get; set; }
        public string LogApiBaseAddress { get; set; }
    }
}
