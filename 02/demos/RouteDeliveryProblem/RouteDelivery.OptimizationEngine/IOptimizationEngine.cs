using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteDelivery.OptimizationEngine
{
    public interface IOptimizationEngine
    {
        List<DeliverySchedule> OptimizeDeliveries(int requestID);
    }
}
