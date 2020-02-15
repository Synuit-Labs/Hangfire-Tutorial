using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.Server.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
            .WriteTo.ColoredConsole()
            .CreateLogger();
           
            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());


            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }

    }
}

