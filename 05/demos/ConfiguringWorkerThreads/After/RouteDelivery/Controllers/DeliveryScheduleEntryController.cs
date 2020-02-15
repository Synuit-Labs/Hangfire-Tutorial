using RouteDelivery.Data;
using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteDelivery.Controllers
{
    public class DriverScheduleEntryController : Controller
    {
        private IUnitOfWork _uof;

        public DriverScheduleEntryController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index(int Id)
        {
            var DriverSchedulesEntries = _uof.DeliveryScheduleEntries.Find(d => d.DeliveryScheduleID == Id);

            if (DriverSchedulesEntries != null)
            {
                return View(DriverSchedulesEntries);
            }
            else
            {
                return RedirectToAction("Index", "DeliveryScheduleController");
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            var DriverScheduleEntry = new DeliveryScheduleEntry();

            return View(DriverScheduleEntry);
        }

        [HttpPost]
        public ActionResult Create(DeliveryScheduleEntry newDriverSchedules)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliveryScheduleEntries.Add(newDriverSchedules);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newDriverSchedules);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var DriverScheduleEdit = _uof.DeliveryScheduleEntries.FindByID(Id);

            return View(DriverScheduleEdit);
        }

        [HttpPost]
        public ActionResult Edit(DeliveryScheduleEntry DriverScheduleEdit)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliveryScheduleEntries.Update(DriverScheduleEdit);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(DriverScheduleEdit);
        }

        public ActionResult Delete(int Id)
        {
            var DriverScheduleDel = _uof.DeliveryScheduleEntries.FindByID(Id);

            if (DriverScheduleDel != null)
            {
                _uof.DeliveryScheduleEntries.Remove(DriverScheduleDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}