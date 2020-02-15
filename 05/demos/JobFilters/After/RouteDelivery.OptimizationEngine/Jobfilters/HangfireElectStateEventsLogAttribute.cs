using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;

namespace RouteDelivery.OptimizationEngine.Jobfilters
{
    public class HangfireElectStateEventsLogAttribute: JobFilterAttribute, IElectStateFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void OnStateElection(ElectStateContext context)
        {
            var failedState = context.CandidateState as FailedState;
            if (failedState != null)
            {
                Logger.WarnFormat(
                    "IElectStateFilter: Job `{0}` has been failed due to an exception `{1}`",
                    context.BackgroundJob.Id,
                    failedState.Exception);
            }
        }

    }
}
