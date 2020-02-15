using Hangfire;
using Microsoft.Owin;
using Owin;
using Serilog;

[assembly: OwinStartupAttribute(typeof(RouteDelivery.Startup))]
namespace RouteDelivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Log.Logger = new LoggerConfiguration().WriteTo.RollingFile(@"C:\Serilogs\RouteDelivery-{Date}.txt")
            .CreateLogger();

            Log.Information("Route Delivery Started");

            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");

            app.UseHangfireDashboard();
            //app.UseHangfireServer();

            DIConfig.Setup();
        }
    }
}
