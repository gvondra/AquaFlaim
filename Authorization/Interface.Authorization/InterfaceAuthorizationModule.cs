using Autofac;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Authorization
{
    public class InterfaceAuthorizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<RestUtil>().SingleInstance();
            builder.RegisterType<Service>().SingleInstance().As<IService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}
