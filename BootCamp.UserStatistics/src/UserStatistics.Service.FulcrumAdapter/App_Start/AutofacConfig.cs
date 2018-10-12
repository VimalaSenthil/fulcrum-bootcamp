﻿using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using UserStatistics.Service.FulcrumAdapter.RestClients;
using Xlent.Lever.Libraries2.Core.Application;
using Xlent.Lever.Libraries2.Core.MultiTenant.Context;
using Xlent.Lever.Libraries2.Core.MultiTenant.Model;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.MemoryStorage;

#pragma warning disable 1591

namespace UserStatistics.Service.FulcrumAdapter
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CrudMemory<Contract.UserStatistics, string>>().As<ICrud<Contract.UserStatistics, string>>().SingleInstance();

            builder.RegisterType<TenantConfigurationValueProvider>().As<ITenantConfigurationValueProvider>().SingleInstance();

            var organization = ConfigurationManager.AppSettings["Organization"];
            var environment = ConfigurationManager.AppSettings["Environment"];
            var tenant = new Tenant(organization, environment);
            builder.RegisterInstance(tenant).As<Tenant>();

            var apiClient = new ApiClient(ConfigurationManager.AppSettings["Api.Url"]);
            FulcrumApplication.Setup.FullLogger = apiClient;
            builder.RegisterInstance(apiClient).As<IApiClient>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}