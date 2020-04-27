﻿using Autofac;
using ElecronicsStore.DB.Storages;
using ElectronicsStore.API.Controllers;
using ElectronicsStore.Core.ConfigurationOptions;
using ElectronicsStore.Repository;

namespace ElectronicsStore.API.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportStorage>().As<IReportStorage>();
            builder.RegisterType<ReportController>().As<IReportController>();
            builder.RegisterType<StorageOptions>().As<IStorageOptions>();
            builder.RegisterType<ReportRepository>().As<IReportRepository>();
        }
    }
}