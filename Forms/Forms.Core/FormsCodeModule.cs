using AquaFlaim.Forms.Framework;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Core
{
    public class FormsCodeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FormTypeFactory>().As<IFormTypeFactory>();
            builder.RegisterType<FormTypeSaver>().As<IFormTypeSaver>();
        }
    }
}
