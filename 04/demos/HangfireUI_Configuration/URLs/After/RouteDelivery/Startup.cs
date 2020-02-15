using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(RouteDelivery.Startup))]
namespace RouteDelivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");

            var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("/OptimizationRequest/Index") };

            app.UseHangfireDashboard("/RouteDeliveryOptimizationJobs", options);
            app.UseHangfireServer();

            DIConfig.Setup();
        }
    }
}
