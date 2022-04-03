namespace FormsAPI
{
    public interface ISettingsFactory
    {
        AquaFlaim.CommonCore.ISettings CreateCore(Settings settings);
        AquaFlaim.Interface.Log.ISettings CreateLog(Settings settings, string token);
    }
}
