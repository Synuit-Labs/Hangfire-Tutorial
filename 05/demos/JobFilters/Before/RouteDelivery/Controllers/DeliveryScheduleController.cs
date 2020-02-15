using RouteDelivery.Data;
using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteDelivery.Controllers
{
    public class DeliveryScheduleController : Controller
    {
        private IUnitOfWork _uof;

        public DeliveryScheduleController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index()
        {
            var DeliverySchedules = _uof.DeliverySchedules.FindAll();

            return View(DeliverySchedules);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var DeliverySchedule = new DeliverySchedule();

            return View(DeliverySchedule);
        }

        [HttpPost]
        public ActionResult Create(DeliverySchedule newDeliverySchedules)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliverySchedules.Add(newDeliverySchedules);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newDeliverySchedules);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var DeliveryScheduleEdit = _uof.DeliverySchedules.FindByID(Id);

            return View(DeliveryScheduleEdit);
        }

        [HttpPost]
        public ActionResult Edit(DeliverySchedule DeliveryScheduleEdit)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliverySchedules.Update(DeliveryScheduleEdit);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(DeliveryScheduleEdit);
        }

        public ActionResult Delete(int Id)
        {
            var DeliveryScheduleDel = _uof.DeliverySchedules.FindByID(Id);

            if (DeliveryScheduleDel != null)
            {
                _uof.DeliverySchedules.Remove(DeliveryScheduleDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}