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
    public class DestinationsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: Destinations
        public ActionResult Index()
        {
            return View(db.Destinations.ToList());  // part2 of Lab02
            //return View(PopulateDestination());
        }

        /*
        private List<Destination> PopulateDestination()
        {
            var destinations = new List<Destination>();
            destinations.Add(new Destination { City = "Beijing", ProvinceCode = "BJ" });
            destinations.Add(new Destination { City = "Changchun", ProvinceCode = "JL" });
            destinations.Add(new Destination { City = "Changsha", ProvinceCode = "HN" });
            destinations.Add(new Destination { City = "Chengdu", ProvinceCode = "SC" });
            destinations.Add(new Destination { City = "Chongqing", ProvinceCode = "CQ" });
            destinations.Add(new Destination { City = "Fuzhou", ProvinceCode = "JX" });
            destinations.Add(new Destination { City = "Golmud", ProvinceCode = "QH" });
            destinations.Add(new Destination { City = "Guangzhou", ProvinceCode = "GD" });
            destinations.Add(new Destination { City = "Guiyang", ProvinceCode = "GZ" });
            destinations.Add(new Destination { City = "Haikou", ProvinceCode = "HI" });
            destinations.Add(new Destination { City = "Hailar", ProvinceCode = "NM" });
            destinations.Add(new Destination { City = "Hangzhou", ProvinceCode = "ZJ" });
            destinations.Add(new Destination { City = "Harbin", ProvinceCode = "HL" });
            destinations.Add(new Destination { City = "Hefei", ProvinceCode = "AH" });
            destinations.Add(new Destination { City = "Hohhot", ProvinceCode = "NM" });
            destinations.Add(new Destination { City = "Hong Kong", ProvinceCode = "HK" });
            destinations.Add(new Destination { City = "Hulun Buir", ProvinceCode = "NM" });
            destinations.Add(new Destination { City = "Jinan", ProvinceCode = "SD" });
            destinations.Add(new Destination { City = "Kashi", ProvinceCode = "XJ" });
            destinations.Add(new Destination { City = "Kunming", ProvinceCode = "YN" });
            destinations.Add(new Destination { City = "Lanzhou", ProvinceCode = "GS" });
            destinations.Add(new Destination { City = "Lhasa", ProvinceCode = "XZ" });
            destinations.Add(new Destination { City = "Macau", ProvinceCode = "MC" });
            destinations.Add(new Destination { City = "Nanchang", ProvinceCode = "JX" });
            destinations.Add(new Destination { City = "Nanjing", ProvinceCode = "JS" });
            destinations.Add(new Destination { City = "Nanning", ProvinceCode = "JX" });
            destinations.Add(new Destination { City = "Qiqihar", ProvinceCode = "HL" });
            destinations.Add(new Destination { City = "Shanghai", ProvinceCode = "SH" });
            destinations.Add(new Destination { City = "Shenyang", ProvinceCode = "LN" });
            destinations.Add(new Destination { City = "Shijiazhuang", ProvinceCode = "HE" });
            destinations.Add(new Destination { City = "Taipei", ProvinceCode = "TW" });
            destinations.Add(new Destination { City = "Taiyuan", ProvinceCode = "SX" });
            destinations.Add(new Destination { City = "Tianjin", ProvinceCode = "HE" });
            destinations.Add(new Destination { City = "Urumqi", ProvinceCode = "XJ" });
            destinations.Add(new Destination { City = "Wuhan", ProvinceCode = "HB" });
            destinations.Add(new Destination { City = "Xi'an", ProvinceCode = "SN" });
            destinations.Add(new Destination { City = "Xining", ProvinceCode = "QH" });
            destinations.Add(new Destination { City = "Yinchuan", ProvinceCode = "NX" });
            destinations.Add(new Destination { City = "Yumen", ProvinceCode = "GS" });
            destinations.Add(new Destination { City = "Zhengzhou", ProvinceCode = "HA" });
            return (destinations);
        }
  */


        // GET: Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // GET: Destinations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DestinationID,City,ProvinceCode")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DestinationID,City,ProvinceCode")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            db.Destinations.Remove(destination);
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
