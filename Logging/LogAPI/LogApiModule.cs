using Autofac;
using AutoMapper;

namespace LogAPI
{
    public class LogApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new AquaFlaim.Log.Data.LogDataModule());
            builder.Register<IMapper>((context) => MapperConfiguration.CreateMapper());
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();    
        }
    }
}
