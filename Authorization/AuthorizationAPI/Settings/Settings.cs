namespace AuthorizationAPI
{
    public class Settings
    {
        public string TknCsp { get; set; }
        public string ConnectionString { get; set; }
        public string ConnectionStringUser { get; set; }
        public bool EnableDatabaseAccessToken { get; set; } = false;
        public string KeyVaultAddress { get; set; }
        public string ExternalIdIssuer { get; set; }
        public string InternalIdIssuer { get; set; }
        public string LogApiBaseAddress { get; set; }
        public string SuperUser { get; set; }
    }
}
