namespace LogAPI
{
    public interface ISettingsFactory
    {
        AquaFlaim.CommonCore.ISettings CreateCore(Settings settings);
        BrassLoon.DataClient.ISqlSettings CreateData(Settings settings);
    }
}
