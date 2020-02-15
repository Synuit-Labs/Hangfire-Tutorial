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
        private IBackgroundJobClient _backgroundJobClient;

        public OptimizationRequestController(IUnitOfWork uof, IOptimizationEngine oe, IBackgroundJobClient backgroundJobClient)
        {
            _uof = uof;
            _optiEngine = oe;
            _backgroundJobClient = backgroundJobClient;
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

                var id = _backgroundJobClient.Schedule(() => _optiEngine.OptimizeDeliveries(newOptimizationRequest.ID), 
                                        TimeSpan.FromMinutes(newOptimizationRequest.OptimizeAfterMinuntes));

                return RedirectToAction("Index");
            }

            return View(newOptimizationRequest);
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

                return RedirectToAction("Index");
            }

            return View(OptimizationRequestEdit);
        }

        public ActionResult Delete(int Id)
        {
            var OptimizationRequestDel = _uof.OptimizationRequests.FindByID(Id);

            if (OptimizationRequestDel != null)
            {
                _uof.OptimizationRequests.Remove(OptimizationRequestDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }

}