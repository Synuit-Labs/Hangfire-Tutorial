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
    public class HangfireApplyStateEventsLogAttribute: JobFilterAttribute, IApplyStateFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            Logger.InfoFormat(
                "IApplyStateFilter: Job `{0}` state was changed from `{1}` to `{2}`",
                context.BackgroundJob.Id,
                context.OldStateName,
                context.NewState.Name);
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            Logger.InfoFormat(
                "IApplyStateFilter: Job `{0}` state `{1}` was unapplied.",
                context.BackgroundJob.Id,
                context.OldStateName);
        }
    }
}
