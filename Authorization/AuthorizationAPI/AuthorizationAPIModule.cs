using Autofac;

namespace AuthorizationAPI
{
    public class AuthorizationAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new AquaFlaim.Authorization.Core.AuthorizationCoreModule());
            builder.RegisterModule(new AquaFlaim.Authorization.Data.AuthorizationDataModule());
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();
        }
    }
}
