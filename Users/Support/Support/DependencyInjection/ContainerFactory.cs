using Autofac;
namespace AquaFlaim.User.Support.DependencyInjection
{
    public static class ContainerFactory
    {
        private static readonly IContainer _container;

        static ContainerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new SupportModule());
            builder.RegisterModule(new AquaFlaim.Interface.Authorization.InterfaceAuthorizationModule());
            builder.RegisterModule(new AquaFlaim.Interface.Log.InterfaceLogModule());
            _container = builder.Build();
        }

        public static IContainer Container => _container;
    }
}
