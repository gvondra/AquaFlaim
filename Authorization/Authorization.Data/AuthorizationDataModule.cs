using AquaFlaim.Authorization.Data.Framework;
using Autofac;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Authorization.Data
{
    public class AuthorizationDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register<IDbProviderFactory>((context) => new BrassLoon.DataClient.SqlClientProviderFactory());
            builder.RegisterType<EmailAddressDataFactory>().As<IEmailAddressDataFactory>();
            builder.RegisterType<EmailAddressDataSaver>().As<IEmailAddressDataSaver>();
            builder.RegisterType<RoleDataFactory>().As<IRoleDataFactory>();
            builder.RegisterType<RoleDataSaver>().As<IRoleDataSaver>();
            builder.RegisterType<UserDataFactory>().As<IUserDataFactory>();
            builder.RegisterType<UserDataSaver>().As<IUserDataSaver>();
        }
    }
}
