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
    public class PeresonalShippingAccountsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();
        /*
                // GET: BusinessShippingAccounts
                public ActionResult Index()
                {
                    return View(db.ShippingAccounts.ToList());
                }
        */
        /*
                // GET: BusinessShippingAccounts/Details/5
                public ActionResult Details(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount) db.ShippingAccounts.Find(id);
                    if (businessShippingAccount == null)
                    {
                        return HttpNotFound();
                    }
                    return View(businessShippingAccount);
                }
        */
        // GET: BusinessShippingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessShippingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingAccountId,Building,Street,City,ProvinceCode,PostalCode,Type,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,PhoneNumber,EmailAddress,AccountNumber,ContactPersonName,CompanyName,DepartmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                //db.ShippingAccounts.Add(businessShippingAccount);
                //db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(businessShippingAccount);
        }

        // GET: BusinessShippingAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccounts.Find(id);
            if (personalShippingAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalShippingAccount);
        }

        // POST: BusinessShippingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingAccountId,Building,Street,City,ProvinceCode,PostalCode,Type,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,PhoneNumber,EmailAddress,AccountNumber,ContactPersonName,CompanyName,DepartmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                businessShippingAccount.UserName = User.Identity.Name;  // newly added for User Profile
                db.Entry(businessShippingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplayInfo", "Account");  // newly added for User Profile
                //return RedirectToAction("Index");     //original
            }
            return View(businessShippingAccount);
        }
        /*
                // GET: BusinessShippingAccounts/Delete/5
                public ActionResult Delete(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    BusinessShippingAccount businessShippingAccount = db.ShippingAccounts.Find(id);
                    if (businessShippingAccount == null)
                    {
                        return HttpNotFound();
                    }
                    return View(businessShippingAccount);
                }
        */
        // POST: BusinessShippingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount)db.ShippingAccounts.Find(id);
            db.ShippingAccounts.Remove(businessShippingAccount);
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
