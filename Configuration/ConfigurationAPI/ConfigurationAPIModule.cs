using Autofac;
namespace ConfigurationAPI
{
    public class ConfigurationAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new AquaFlaim.Config.Data.ConfigDataModule());
            builder.RegisterModule(new AquaFlaim.Config.Core.ConfigModule());
            builder.RegisterModule(new AquaFlaim.Interface.Log.InterfaceLogModule());
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();
        }
    }
}
