using Hangfire;
using Microsoft.Owin;
using Owin;
using RouteDelivery.Hangfire;

[assembly: OwinStartupAttribute(typeof(RouteDelivery.Startup))]
namespace RouteDelivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");
    
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseHangfireServer();

            DIConfig.Setup();
        }
    }
}
