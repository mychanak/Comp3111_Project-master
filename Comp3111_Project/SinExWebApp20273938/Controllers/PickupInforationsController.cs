using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20273938.Models;

namespace SinExWebApp20273938.Controllers
{
    public class PickupInforationsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: PickupInforations
        public ActionResult Index()
        {
            var pickupInforations = db.PickupInforations.Include(p => p.Shipment);
            return View(pickupInforations.ToList());
        }

        // GET: PickupInforations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupInforation pickupInforation = db.PickupInforations.Find(id);
            if (pickupInforation == null)
            {
                return HttpNotFound();
            }
            return View(pickupInforation);
        }

        // GET: PickupInforations/Create
        public ActionResult Create()
        {
            ViewBag.Pickuptype = new List<SelectListItem> {
                       new SelectListItem { Value = "Immdiate" , Text = "Immdiate" },
                       new SelectListItem { Value = "Prearangement" , Text = "Prearrangment" }

                    };
            return View();
        }

        // POST: PickupInforations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WaybillId,Pickuptype,Pickuplocation,PickupDateTime")] PickupInforation pickupInforation)
        {
            if (ModelState.IsValid)
            {
                pickupInforation.WaybillId= int.Parse(Session["WaybillId"].ToString());
                db.PickupInforations.Add(pickupInforation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", pickupInforation.WaybillId);
            return View("Index","Shipments");
        }

        // GET: PickupInforations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupInforation pickupInforation = db.PickupInforations.Find(id);
            if (pickupInforation == null)
            {
                return HttpNotFound();
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", pickupInforation.WaybillId);
            return View(pickupInforation);
        }

        // POST: PickupInforations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WaybillId,Pickuptype,Pickuplocation,PickupDateTime")] PickupInforation pickupInforation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickupInforation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", pickupInforation.WaybillId);
            return View(pickupInforation);
        }

        // GET: PickupInforations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupInforation pickupInforation = db.PickupInforations.Find(id);
            if (pickupInforation == null)
            {
                return HttpNotFound();
            }
            return View(pickupInforation);
        }

        // POST: PickupInforations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickupInforation pickupInforation = db.PickupInforations.Find(id);
            db.PickupInforations.Remove(pickupInforation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
