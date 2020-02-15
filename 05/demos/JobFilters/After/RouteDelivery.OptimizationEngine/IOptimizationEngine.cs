using Hangfire;

namespace RouteDelivery.OptimizationEngine
{
    public interface IOptimizationEngine
    {
        void OptimizeDeliveries(IJobCancellationToken cancellationToken, OptimizeDeliveriesRequest request);
    }
}
