﻿using System;
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
    public class HangfireServerEventsLogAttribute: JobFilterAttribute, IServerFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void OnPerforming(PerformingContext context)
        {
            Logger.InfoFormat("IServerFilter: Starting to perform job `{0}`", context.BackgroundJob.Id);
        }

        public void OnPerformed(PerformedContext context)
        {
            Logger.InfoFormat("IServerFilter: Job `{0}` has been performed", context.BackgroundJob.Id);
        }
    }
}
