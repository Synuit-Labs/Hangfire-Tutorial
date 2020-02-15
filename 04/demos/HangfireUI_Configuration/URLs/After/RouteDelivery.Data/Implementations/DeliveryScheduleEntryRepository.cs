using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteDelivery.Data.Implementations
{
    public class DeliveryScheduleEntryRepository: IRepository<DeliveryScheduleEntry>
    {
        private EDM.RouteDelivery _dbContext;

        public DeliveryScheduleEntryRepository(EDM.RouteDelivery dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.DeliveryScheduleEntries.Count() == 0)
            {
                //AddRange(GetMockDeliveryScheduleData());
                //_dbContext.SaveChanges();
            }
        }

        private List<DeliveryScheduleEntry> GetMockDeliveryScheduleData()
        {
            return new List<DeliveryScheduleEntry>() {
                new DeliveryScheduleEntry() { DeliveryScheduleID = 1, CustomerID = 12001, DriverName = "Rag", PackageID = 1, TransportType = Models.Type.TransportType.Heavy, EstimatedTime = new DateTime(2016,01,01,10,0,0)}
            };
        }

        public void Add(DeliveryScheduleEntry newEntity)
        {
            _dbContext.DeliveryScheduleEntries.Add(newEntity);
        }

        public List<DeliveryScheduleEntry> Find(Func<DeliveryScheduleEntry, bool> match)
        {
            return _dbContext.DeliveryScheduleEntries.Where(match).ToList();
        }

        public List<DeliveryScheduleEntry> FindAll()
        {
            return _dbContext.DeliveryScheduleEntries.ToList();
        }

        public void Remove(DeliveryScheduleEntry entity)
        {
            _dbContext.DeliveryScheduleEntries.Remove(entity);
        }

        public void Update(DeliveryScheduleEntry entity)
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public DeliveryScheduleEntry FindByID(int id)
        {
            var results = _dbContext.DeliveryScheduleEntries.Where(d => d.ID == id);

            return results.FirstOrDefault();
        }

        public void AddRange(List<DeliveryScheduleEntry> scheduledDeliveries)
        {
            _dbContext.DeliveryScheduleEntries.AddRange(scheduledDeliveries);        
        }
    }
}