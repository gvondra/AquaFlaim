using Autofac;
using AutoMapper;
namespace AuthorizationAPI
{
    public class AuthorizationAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new AquaFlaim.Authorization.Core.AuthorizationCoreModule());
            builder.RegisterModule(new AquaFlaim.Authorization.Data.AuthorizationDataModule());
            builder.RegisterModule(new AquaFlaim.Interface.Authorization.InterfaceAuthorizationModule());
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();
            builder.Register<IMapper>((context) => MapperConfiguration.CreateMapper());
        }
    }
}
