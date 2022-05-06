using AquaFlaim.Config.Framework;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Config.Core
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ItemFactory>().As<IItemFactory>();
            builder.RegisterType<ItemSaver>().As<IItemSaver>();
            builder.RegisterType<LookupFactory>().As<ILookupFactory>();
            builder.RegisterType<LookupSaver>().As<ILookupSaver>();
        }
    }
}
