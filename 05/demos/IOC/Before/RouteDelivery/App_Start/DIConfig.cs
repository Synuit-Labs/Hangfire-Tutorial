using Autofac;
using Autofac.Integration.Mvc;
using RouteDelivery.Data;
using RouteDelivery.Data.Implementations;
using System;
using System.Web.Mvc;
using RouteDelivery.OptimizationEngine;
using Hangfire;

namespace RouteDelivery
{
    internal class DIConfig
    {
        internal static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerBackgroundJob();
            builder.RegisterType<OptimizationEngine.OptimizationEngine>().As<IOptimizationEngine>()
                .WithParameter("uof", new UnitOfWork());
            builder.RegisterType<OptimizationEngine.OptimizationEngine>();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
        }
    }
}