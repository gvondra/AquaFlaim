using AquaFlaim.Forms.Data.Framework;
using Autofac;
using BrassLoon.DataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.Forms.Data
{
    public class FormsDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SqlClientProviderFactory>().SingleInstance().As<ISqlDbProviderFactory>();
            builder.RegisterType<FormQuestionTypeDataFactory>().As<IFormQuestionTypeDataFactory>();
            builder.RegisterType<FormQuestionTypeDataSaver>().As<IFormQuestionTypeDataSaver>();
            builder.RegisterType<FormSectionTypeDataFactory>().As<IFormSectionTypeDataFactory>();
            builder.RegisterType<FormSectionTypeDataSaver>().As<IFormSectionTypeDataSaver>();
            builder.RegisterType<FormTypeDataFactory>().As<IFormTypeDataFactory>();
            builder.RegisterType<FormTypeDataSaver>().As<IFormTypeDataSaver>();
        }
    }
}
