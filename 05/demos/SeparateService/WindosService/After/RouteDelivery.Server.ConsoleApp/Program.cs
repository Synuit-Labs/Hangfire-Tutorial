using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace RouteDelivery.Server.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Serilog Configuration: Log to console and file
            Log.Logger = new LoggerConfiguration()
            .WriteTo.ColoredConsole()
            .WriteTo.RollingFile(@"C:\Serilogs\RouteDelivery-Service-{Date}.txt")
            .CreateLogger();
         
            //Hangfire Job Storage 
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireR3");

            //Topshelf
            HostFactory.Run(x =>                                 
            {
                x.UseSerilog();

                x.Service<RouteDeliveryService>(s =>                        
                {
                    s.ConstructUsing(name => new RouteDeliveryService());     
                    s.WhenStarted(rs => rs.Start());              
                    s.WhenStopped(rs => rs.Stop());               
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Route Delivery Service");        
                x.SetDisplayName("RouteDeliveryService");                       
                x.SetServiceName("RouteDeliveryService");                       
            });
        }

    }
}

