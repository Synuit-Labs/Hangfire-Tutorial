using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(RouteDelivery.Startup))]
namespace RouteDelivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var options = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(60), // Default value 15
                PrepareSchemaIfNecessary = false,
                
            };

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3", options);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            DIConfig.Setup();
        }
    }
}
