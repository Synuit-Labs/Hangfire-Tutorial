using Hangfire.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteDelivery.Hangfire
{
    public class CustomHangfireLogProvider : ILogProvider
    {
        public ILog GetLogger(string name)
        {
            return new CustomHangfireLogger();
        }
    }
}