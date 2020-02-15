using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultipleDashboards.Startup))]
namespace MultipleDashboards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var storage1 = new SqlServerStorage("HangfireR3");
            var storage2 = new SqlServerStorage("HangfireR4");

            app.UseHangfireDashboard("/hangfire1", new DashboardOptions(), storage1);
            app.UseHangfireDashboard("/hangfire2", new DashboardOptions(), storage2);

            //app.UseHangfireServer();
        }
    }
}
