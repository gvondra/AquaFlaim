using Autofac;
using BrassLoon.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Interface.Log
{
    public class InterfaceLogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<RestUtil>().SingleInstance();
            builder.RegisterType<Service>().As<IService>();
            builder.RegisterType<ExceptionService>().As<IExceptionService>();
            builder.RegisterType<MetricService>().As<IMetricService>();
            builder.RegisterType<TraceService>().As<ITraceService>();
        }
    }
}
