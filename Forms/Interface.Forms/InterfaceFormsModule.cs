using Autofac;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Forms
{
    public class InterfaceFormsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<RestUtil>().SingleInstance();
            builder.RegisterType<Service>().As<IService>();
            builder.RegisterType<FormTypeService>().As<IFormTypeService>();
        }
    }
}
