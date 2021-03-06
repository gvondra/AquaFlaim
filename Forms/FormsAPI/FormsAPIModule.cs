using Autofac;
namespace FormsAPI
{
    public class FormsAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new AquaFlaim.Forms.Data.FormsDataModule());
            builder.RegisterModule(new AquaFlaim.Forms.Core.FormsCodeModule());
            builder.RegisterModule(new AquaFlaim.Interface.Log.InterfaceLogModule());
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();    
        }
    }
}
