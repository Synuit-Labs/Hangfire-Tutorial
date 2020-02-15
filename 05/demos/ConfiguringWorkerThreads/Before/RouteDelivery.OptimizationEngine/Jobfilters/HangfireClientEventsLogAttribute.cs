using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.Common;
using Hangfire.Client;
using Hangfire.Logging;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;

namespace RouteDelivery.OptimizationEngine.Jobfilters
{
    public class HangfireClientEventsLogAttribute : JobFilterAttribute, IClientFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

            public void OnCreating(CreatingContext context)
            {
                Logger.InfoFormat("IClientFilter: Creating a job based on method `{0}`...", context.Job.Method.Name);
            }

            public void OnCreated(CreatedContext context)
            {
                Logger.InfoFormat(
                    "IClientFilter: Job that is based on method `{0}` has been created with id `{1}`",
                    context.Job.Method.Name,
                    context.BackgroundJob?.Id);
            }

    }
}