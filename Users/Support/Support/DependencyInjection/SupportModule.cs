using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.DependencyInjection
{
    public class SupportModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();
            builder.RegisterType<Forms.ViewModel.TypeLoader>();
        }
    }
}
