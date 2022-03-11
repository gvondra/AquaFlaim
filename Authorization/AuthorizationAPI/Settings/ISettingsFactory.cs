namespace AuthorizationAPI
{
    public interface ISettingsFactory
    {
        AquaFlaim.CommonCore.ISettings CreateCore(Settings settings);
    }
}
