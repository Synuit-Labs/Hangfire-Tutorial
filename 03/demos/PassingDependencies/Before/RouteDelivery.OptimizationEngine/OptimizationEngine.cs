using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RouteDelivery.Data;
using RouteDelivery.Data.Implementations;
using static RouteDelivery.Models.OptimizationRequest;

namespace RouteDelivery.OptimizationEngine
{
    public class OptimizeDeliveriesRequest
    {
        public int RequestID { get; set; }
        public DateTime ScheduleDate { get; set; }
    }

    public class OptimizationEngine: IOptimizationEngine
    {
        private Random _rnd = new Random();
        private IUnitOfWork _uof;

        public OptimizationEngine()
        {
            _uof = new UnitOfWork();
        }

        public OptimizationEngine(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public void OptimizeDeliveries(OptimizeDeliveriesRequest optimizeDeliveriesRequest)
        {
            var request = GetRequest(optimizeDeliveriesRequest.RequestID);
            var customers = GetCustomers(request);
            var deliveries = GetDeliveries(request);
            var drivers = GetDrivers(request);

            var optimizedDeliveryScheduleEntries = new List<DeliveryScheduleEntry>();
            var deliverySchedule = new DeliverySchedule() { OptimizationRecurringJobID = optimizeDeliveriesRequest.RequestID, ScheduleDate = optimizeDeliveriesRequest.ScheduleDate };
            _uof.DeliverySchedules.Add(deliverySchedule);
            request.Status = RequestStatus.Processing;
            _uof.SaveChanges();
            var deliveryNo = 0;

            foreach (var c in customers)
            {
                var customerDistanceFromWareHouse = GetCustomerDistanceFromWareHouse(c);
                deliveryNo = 0;

                foreach (var d in deliveries.Where(d => d.CustomerID == c.ID))
                {
                    var idealDriver = GetIdealDriver(drivers, d.TransportType, customerDistanceFromWareHouse);

                    deliveryNo++;

                    if (idealDriver != null)
                    {
                        optimizedDeliveryScheduleEntries.Add(new DeliveryScheduleEntry() { CustomerID = c.ID, DriverName = idealDriver.DriverName, DeliveryScheduleID = deliverySchedule.ID, PackageID = d.ID, TransportType = d.TransportType, EstimatedTime = deliverySchedule.ScheduleDate.AddHours(deliveryNo), ID = deliveryNo});
                    }
                }
            }

            request.Status = RequestStatus.Scheduled;
            _uof.DeliveryScheduleEntries.AddRange(optimizedDeliveryScheduleEntries);
            _uof.SaveChanges();
        }

        private DateTime GetSchedDate()
        {
            DateTime SchedDateTime = System.DateTime.Now;

            if(_uof.DeliverySchedules.FindAll().Count>0)
            {
                SchedDateTime = System.DateTime.Now.Date.AddDays(_uof.DeliverySchedules.FindAll().Max(d => d.ID) + 1);
            }

            return SchedDateTime;
        }

        #region Get Data Related To Request
        private IEnumerable<Driver> GetDrivers(object request)
        {
            return _uof.Drivers.FindAll();
        }

        private IEnumerable<Delivery> GetDeliveries(object request)
        {
            return _uof.Deliveries.FindAll();
        }

        private IEnumerable<Customer> GetCustomers(object request)
        {
            return _uof.Customers.FindAll();
        }

        private OptimizationRequest GetRequest(int requestID)
        {
            return _uof.OptimizationRequests.FindByID(requestID);
        }
        #endregion

        #region Optimize Delivery Helper Methods
        private Driver GetIdealDriver(IEnumerable<Driver> drivers, Models.Type.TransportType transportType, int customerDistanceFromWareHouse)
        {
            Thread.Sleep(500);
            return drivers.FirstOrDefault(d => d.TransportType == transportType && _rnd.Next(1, 4) == _rnd.Next(1, 4));
        }

        private int GetCustomerDistanceFromWareHouse(Customer c)
        {
            Thread.Sleep(500);
            return _rnd.Next(1, 100);
        }
        #endregion

    }
}
