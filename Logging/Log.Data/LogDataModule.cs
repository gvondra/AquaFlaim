using AquaFlaim.Log.Data.Framework;
using Autofac;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Log.Data
{
    public class LogDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register<IDbProviderFactory>((context) => new BrassLoon.DataClient.SqlClientProviderFactory());
            builder.RegisterType<ExceptionDataSaver>().As<IExceptionDataSaver>();
            builder.RegisterType<MetricDataFactory>().As<IMetricDataFactory>();
            builder.RegisterType<MetricDataSaver>().As<IMetricDataSaver>();
            builder.RegisterType<TraceDataSaver>().As<ITraceDataSaver>();
        }
    }
}
