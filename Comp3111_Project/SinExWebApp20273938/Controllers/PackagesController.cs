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
    public class PackagesController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: Packages
        public ActionResult Index()
        {
            int id= int.Parse(Session["WaybillId"].ToString());
            var packages = db.Packages.Include(p => p.PackageType).Include(p => p.Shipment).Where(p=>p.WaybillId==id);
            return View(packages.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type");
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageId,PackageTypeID,Description,Value,Weight,WaybillId")] Package package)
        {
            if (ModelState.IsValid)
            {
                package.WaybillId = int.Parse(Session["WaybillId"].ToString());
                
                db.Packages.Add(package);
                db.SaveChanges();
                Shipment shipment = db.Shipments.Find(package.WaybillId);
                shipment.NumberOfPackages++;
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", package.PackageTypeID);
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", package.PackageTypeID);
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", package.WaybillId);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageId,PackageTypeID,Description,Value,Weight,WaybillId")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", package.PackageTypeID);
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", package.WaybillId);
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Package package = db.Packages.Find(id);
            package.WaybillId = int.Parse(Session["WaybillId"].ToString());

            Shipment shipment = db.Shipments.Find(package.WaybillId);
            shipment.NumberOfPackages--;
            db.Entry(shipment).State = EntityState.Modified;
            db.SaveChanges();
            db.Packages.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddWeight(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("AddWeight")]
        [ValidateAntiForgeryToken]
        public ActionResult AddWeight(int id)
        {

            Package package = db.Packages.Find(id);
        
            db.Entry(package).State = EntityState.Modified;
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
