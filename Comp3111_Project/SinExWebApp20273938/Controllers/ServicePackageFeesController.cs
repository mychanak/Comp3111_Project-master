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
    public class ServicePackageFeesController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: ServicePackageFees
                public ActionResult Index(string OriginChoice, string DestinationChoice, 
            string ServiceTypeChoice, string PackageTypeChoice, decimal? WeightEntered, 
            string CurrentPlace = "CNY")
        {
            /*
            Populate Lists:    
            DestinationList, ServiceTypeList, PackageTypeList are selectList for Fee Calculator dropdownlist
            CurrencyList is selectList for Currency Conversion dropdownlist
            */

            // Populate OriginList and DestinationList
            var DestinationQuery = db.Destinations.Select(s => s.City).Distinct();
            ViewBag.DestinationList = DestinationQuery;

            // Populate ServiceTypeList
            var ServiceTypeQuery = db.ServiceTypes.Select(s => s.Type).Distinct();
            ViewBag.ServiceTypeList = ServiceTypeQuery;

            // Populate PackageTypeList
            var PackageTypeQuery = db.PackageTypes.Select(s => s.Type).Distinct();
            ViewBag.PackageTypeList = PackageTypeQuery;

            // Populate CurrencyList
            var CurrencyQuery = db.Currencies.Select(s => s.CurrencyCode);
            ViewBag.CurrencyList = CurrencyQuery;

            // Make sure the PriceResult did carry out search if choice != null
            if (ServiceTypeChoice != null && PackageTypeChoice != null && WeightEntered != null)
            {
                // Calculation in fee calculator
                ViewBag.TotalPrice = 0;
                foreach (var item in db.ServicePackageFees)
                {
                    // Note that the attribute WeightLimit is added to the PackageTypeSize and SinExConfiguration
                    if (item.ServiceType.Type == ServiceTypeChoice && item.PackageType.Type == PackageTypeChoice)
                    {
                        var PriceByWeight = item.Fee * WeightEntered;
                        var PriceMinimum = item.MinimumFee;
                        ViewBag.TotalPrice = (PriceByWeight > PriceMinimum) ? PriceByWeight : PriceMinimum;
                        break;
                    }
                }
                foreach (var item in db.PackageTypeSizes)
                {
                    if (item.PackageType.Type == PackageTypeChoice )
                    {
                        if (item.WeightLimit != null && item.WeightLimit != 0)
                        {
                            if (WeightEntered > item.WeightLimit)
                            {
                                ViewBag.TotalPrice = ViewBag.TotalPrice + 500;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                ViewBag.TotalPrice = 0;
            }

            // Convert Currency by calling BaseController function
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            foreach (var item in servicePackageFees)
            {
                item.Fee = convertToCurrency(CurrentPlace, item.Fee);
                item.MinimumFee = convertToCurrency(CurrentPlace, item.MinimumFee);
            }
            return View(servicePackageFees.ToList());
        }


        // GET: ServicePackageFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Create
        public ActionResult Create()
        {
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type");
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type");
            return View();
        }

        // POST: ServicePackageFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.ServicePackageFees.Add(servicePackageFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePackageFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            db.ServicePackageFees.Remove(servicePackageFee);
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
