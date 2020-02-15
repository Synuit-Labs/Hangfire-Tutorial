using RouteDelivery.Data;
using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RouteDelivery.OptimizationEngine;
using Hangfire;
using RouteDelivery.Data.Implementations;

namespace RouteDelivery.Data
{
    public class OptimizationRequestController : Controller
    {
        private IUnitOfWork _uof;
        private IOptimizationEngine _optiEngine;

        public OptimizationRequestController()
        {
            _uof = new UnitOfWork();
            _optiEngine = new OptimizationEngine.OptimizationEngine(_uof);
        }

        public ActionResult Index()
        {
            var optimizationRequests = _uof.OptimizationRequests.FindAll();

            return View(optimizationRequests);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var optimizationRequest= new OptimizationRequest();

            return View(optimizationRequest);
        }

        [HttpPost]
        public ActionResult Create(OptimizationRequest newOptimizationRequest)
        {
            if (ModelState.IsValid)
            {
                _uof.OptimizationRequests.Add(newOptimizationRequest);
                _uof.SaveChanges();

                var cronType = GetCronFromRecurringType(newOptimizationRequest.RecurringSchedule);
                RecurringJob.AddOrUpdate(newOptimizationRequest.ID.ToString(), 
                    () => _optiEngine.OptimizeDeliveries(JobCancellationToken.Null, new OptimizeDeliveriesRequest() {  RequestID = newOptimizationRequest.ID, ScheduleDate = newOptimizationRequest.RequestDate}), 
                    cronType);

                return RedirectToAction("Index");
            }

            return View(newOptimizationRequest);
        }

        private Func<string> GetCronFromRecurringType(OptimizationRequest.RecurringScheduleType recurringSchedule)
        {
            switch (recurringSchedule)
            {
                case OptimizationRequest.RecurringScheduleType.Daily:
                    return Cron.Daily;
                case OptimizationRequest.RecurringScheduleType.Hourly:
                    return Cron.Hourly;
                case OptimizationRequest.RecurringScheduleType.Minutely:
                    return Cron.Minutely;
                case OptimizationRequest.RecurringScheduleType.Monthly:
                    return Cron.Monthly;
                case OptimizationRequest.RecurringScheduleType.Weekly:
                    return Cron.Weekly;
                case OptimizationRequest.RecurringScheduleType.Yearly:
                    return Cron.Yearly;
                default:
                    return Cron.Daily;
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var optimizationRequestEdit = _uof.OptimizationRequests.FindByID(Id);

            return View(optimizationRequestEdit);
        }

        [HttpPost]
        public ActionResult Edit(OptimizationRequest OptimizationRequestEdit)
        {
            if (ModelState.IsValid)
            {
                _uof.OptimizationRequests.Update(OptimizationRequestEdit);
                _uof.SaveChanges();

                var cronType = GetCronFromRecurringType(OptimizationRequestEdit.RecurringSchedule);
                RecurringJob.AddOrUpdate(OptimizationRequestEdit.ID.ToString(),
                    () => _optiEngine.OptimizeDeliveries(JobCancellationToken.Null, new OptimizeDeliveriesRequest() { RequestID = OptimizationRequestEdit.ID, ScheduleDate = OptimizationRequestEdit.RequestDate }),
                    cronType);
                return RedirectToAction("Index");
            }

            return View(OptimizationRequestEdit);
        }

        public ActionResult Remove(int Id)
        {
            var OptimizationRequestDel = _uof.OptimizationRequests.FindByID(Id);

            if (OptimizationRequestDel != null)
            {
                OptimizationRequestDel.Status = OptimizationRequest.RequestStatus.Removed;
                _uof.OptimizationRequests.Update(OptimizationRequestDel);
                _uof.SaveChanges();

                RecurringJob.RemoveIfExists(OptimizationRequestDel.ID.ToString());
            }
            return RedirectToAction("Index");
        }

        public ActionResult TriggerNow(int Id)
        {
            RecurringJob.Trigger(Id.ToString());

            return RedirectToAction("Index");
        }

    }

}