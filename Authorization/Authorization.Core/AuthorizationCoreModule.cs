﻿using AquaFlaim.Authorization.Framework;
using Autofac;
namespace AquaFlaim.Authorization.Core
{
    public class AuthorizationCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<EmailAddressFactory>().As<IEmailAddressFactory>();
            builder.RegisterType<EmailAddressSaver>().As<IEmailAddressSaver>();
            builder.RegisterType<UserFactory>().As<IUserFactory>();
            builder.RegisterType<UserSaver>().As<IUserSaver>();
        }
    }
}