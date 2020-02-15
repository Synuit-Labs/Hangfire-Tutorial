using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RouteDelivery.Models;
using System.Diagnostics;
using RouteDriver.Data.Implementations;

namespace RouteDelivery.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Customer> _customers;
        private IRepository<Delivery> _deliveries;
        private IRepository<Driver> _drivers;
        private IRepository<OptimizationRequest> _optimizationRequest;
        private IRepository<DeliveryScheduleEntry> _deliveryScheduleEntries;
        private IRepository<DeliverySchedule> _deliverySchedules;
        private EDM.RouteDelivery _dbContext;

        public UnitOfWork()
        {
            _dbContext = new EDM.RouteDelivery();

            _customers = new CustomerRepository(_dbContext);
            _deliveries = new DeliveryRepository(_dbContext);
            _drivers = new DriverRepository(_dbContext);
            _optimizationRequest = new OptimizationRequestRepository(_dbContext);
            _deliveryScheduleEntries = new DeliveryScheduleEntryRepository(_dbContext);
            _deliverySchedules = new DeliveryScheduleRepository(_dbContext);
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return _customers;
            }
        }

        public IRepository<Delivery> Deliveries
        {
            get
            {
                return _deliveries;
            }
        }

        public IRepository<Driver> Drivers
        {
            get
            {
                return _drivers;
            }
        }

        public IRepository<OptimizationRequest> OptimizationRequests
        {
            get
            {
                return _optimizationRequest;
            }
        }

        public IRepository<DeliveryScheduleEntry> DeliveryScheduleEntries
        {
            get
            {
                return _deliveryScheduleEntries;
            }
        }

        public IRepository<DeliverySchedule> DeliverySchedules
        {
            get
            {
                return _deliverySchedules;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}