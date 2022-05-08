using AquaFlaim.Config.Data.Framework;
using Autofac;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Data
{
    public class ConfigDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SqlClientProviderFactory>().As<ISqlDbProviderFactory>();
            builder.RegisterType<ItemDataFactory>().As<IItemDataFactory>();
            builder.RegisterType<ItemDataSaver>().As<IItemDataSaver>();
            builder.RegisterType<LookupDataFactory>().As<ILookupDataFactory>();
            builder.RegisterType<LookupDataSaver>().As<ILookupDataSaver>();
        }
    }
}
