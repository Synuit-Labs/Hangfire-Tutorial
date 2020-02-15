using Hangfire;
using Hangfire.Logging;
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

            LogProvider.SetCurrentLogProvider(new CustomHangfireLogProvider());

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            DIConfig.Setup();
        }
    }
}
