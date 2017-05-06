using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20273938.Models;
using SinExWebApp20273938.ViewModels;   //lab03 part 1
using X.PagedList;    //lab03 part 2

namespace SinExWebApp20273938.Controllers
{
    public class ShipmentsController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();


    // GET: Shipments/GenerateHistoryReport
    public ActionResult GenerateHistoryReport(int? ShippingAccountId, string sortOrder, string ServiceType, DateTime? ShippedDate, DateTime? DeliveredDate, string RecipientName, string Origin, string Destination,  string currentServiceType, DateTime? currentShippedDate, DateTime? currentDeliveredDate, string currentRecipientName, string currentOrigin, string currentDestination, int? currentShippingAccountId, int? page,
            DateTime? FromShippedDate, DateTime? FromDeliveredDate, DateTime? CURRENTToShippedDate, DateTime? CURRENTToDeliveredDate
             //to be done
             /*string From_Year, string From_Month, string From_Day, string UNTIL_Year, string UNTIL_Month, string UNTIL_Day*/
             )
        {
                // Instantiate an instance of the ShipmentsReportViewModel and the ShipmentsSearchViewModel.
                var shipmentSearch = new ShipmentsReportViewModel();
            shipmentSearch.Shipment = new ShipmentsSearchViewModel();

            //Code for paging
            ViewBag.CurrentSort = sortOrder;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //DateRangeSearch
            if (FromShippedDate == null)
            {
                FromShippedDate = CURRENTToShippedDate;
            }
            else
            {
                page = 1;
            }

            if (FromDeliveredDate == null)
            {
                FromDeliveredDate = CURRENTToDeliveredDate;
            }
            else
            {
                page = 1;
            }

            //Retain search condition for sorting
            if (ShippingAccountId == null) //ServiceType == null && ShippedDate == null && DeliveredDate == null && RecipientName == null && Origin == null && Destination == null
            {
                ShippingAccountId = currentShippingAccountId;
                ServiceType = currentServiceType;
                ShippedDate = currentShippedDate;
                DeliveredDate = currentDeliveredDate;
                RecipientName = currentRecipientName;
                Origin = currentOrigin;
                Destination = currentDestination;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentShippingAccountId = ShippingAccountId.GetValueOrDefault(); //ViewBag.CurrentShippingAccountId = ShippingAccountId; does not make visible error in my understanding

            ViewBag.CurrentServiceType = currentServiceType;
            ViewBag.CurrentShippedDate = currentShippedDate;
            ViewBag.CurrentDeliveredDate = currentDeliveredDate;
            ViewBag.CurrentRecipientName = currentRecipientName;
            ViewBag.CurrentOrigin = currentOrigin;
            ViewBag.CurrentDestination = currentDestination;


            // Populate the ShippingAccountId dropdown list.
            shipmentSearch.Shipment.ShippingAccounts = PopulateShippingAccountsDropdownList().ToList();
            shipmentSearch.Shipment.ShippingAccountId = currentShippingAccountId ?? default (int);

            // Initialize the query to retrieve shipments using the ShipmentsListViewModel.
            var shipmentQuery = from s in db.Shipments
                                //orderby s.WaybillId
                                where ShippingAccountId == s.ShippingAccountId
                                select new ShipmentsListViewModel
                                {
                                    WaybillId = s.WaybillId,
                                    ServiceType = s.ServiceType,
                                    ShippedDate = s.ShippedDate,
                                    DeliveredDate = s.DeliveredDate,
                                    RecipientName = s.RecipientName,
                                    NumberOfPackages = s.NumberOfPackages,
                                    Origin = s.Origin,
                                    Destination = s.Destination,
                                    ShippingAccountId = s.ShippingAccountId
                                };

            //DateRangeSearch
            // Add the condition to select a spefic ShippedDate user input if ShippedDate is not null.
            if (FromShippedDate != null)
            {
                // TODO: Construct the LINQ query to retrive only the shipments for the specified shipping account id.
                shipmentQuery = shipmentQuery.Where(a => a.ShippedDate >= FromShippedDate).OrderBy(b => b.WaybillId);

            }
            // Add the condition to select a spefic ShippedDate user input if ShippedDate is not null.
            if (FromDeliveredDate != null)
            {
                // TODO: Construct the LINQ query to retrive only the shipments for the specified shipping account id.
                shipmentQuery = shipmentQuery.Where(a => a.DeliveredDate <= FromDeliveredDate).OrderBy(b => b.WaybillId);

            }
            if (User.IsInRole("Employee"))
            {
                return View(db.Shipments.ToList());
            }

            // Add the condition to select a spefic shipping account if shipping account id is not null.
            if (ShippingAccountId != null)
            {
                // TODO: Construct the LINQ query to retrive only the shipments for the specified shipping account id.
                shipmentQuery = shipmentQuery.Where( a => a.ShippingAccountId == ShippingAccountId ).OrderBy(b => b.WaybillId ) ;

            }
 /*           else
            {
                // Return an empty result if no shipping account id has been selected.
                shipmentSearch.Shipments = new ShipmentsListViewModel[0];
            }
*/
            //Code for osrting on WaybillId, ServiceType, ShippedDate, DeliveredDate, RecipientName, Origin and Destination
            ViewBag.ServiceTypeSortParm = string.IsNullOrEmpty(sortOrder) ? "ServiceType_desc" : "ServiceType";
            ViewBag.ShippedDateSortParm = string.IsNullOrEmpty(sortOrder) ? "ShippedDate_desc" : "ShippedDate";
            ViewBag.DeliveredDateSortParm = string.IsNullOrEmpty(sortOrder) ? "DeliveredDate_desc" : "DeliveredDate";
            ViewBag.RecipientNameSortParm = string.IsNullOrEmpty(sortOrder) ? "RecipientName_desc" : "RecipientName";
            ViewBag.OriginSortParm = string.IsNullOrEmpty(sortOrder) ? "Origin_desc" : "Origin";
            ViewBag.DestinationSortParm = string.IsNullOrEmpty(sortOrder) ? "Destination_desc" : "Destination";
            switch (sortOrder)
            {             
                case "ServiceType_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.ServiceType);
                    break;
                case "ShippedDate_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.ShippedDate);
                    break;
                case "DeliveredDate_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.DeliveredDate);
                    break;
                case "RecipientName_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.RecipientName);
                    break;
                case "Origin_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.Origin);
                    break;
                case "Destination_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.Destination);
                    break;
                case "ServiceType":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.ServiceType);
                    break;
                case "ShippedDate":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.ShippedDate);
                    break;
                case "DeliveredDate":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.DeliveredDate);
                    break;
                case "RecipientName":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.RecipientName);
                    break;
                case "Origin":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.Origin);
                    break;
                case "Destination":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.Destination);
                    break;
                default:
                    break;
            }
            //  shipmentSearch.Shipments = shipmentQuery.ToList();
            shipmentSearch.Shipments = shipmentQuery.ToPagedList(pageNumber, pageSize);
            return View(shipmentSearch);
        }

        private SelectList PopulateShippingAccountsDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var shippingAccountQuery = db.Shipments.Select( s => s.ShippingAccountId ).Distinct().OrderBy( c => c ) ;
            return new SelectList(shippingAccountQuery);
        }

        /*
        private SelectList PopulateDateRangeSearchDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var shippingAccountQuery = db.Shipments.Select(s => s.ShippingAccountId).Distinct().OrderBy(c => c);
            return new SelectList(shippingAccountQuery);
        }
        */

        // GET: Shipments
        public ActionResult Index()
        {
            if (User.IsInRole("Employee")) {
                return View(db.Shipments.ToList());
            }
            int ShippingAccountId = db.ShippingAccounts.Where(s => s.UserName == User.Identity.Name).Select(s => s.ShippingAccountId).Single();
            var shipment = db.Shipments.Include(p => p.Packages).Where(p=>p.ShippingAccountId==ShippingAccountId);
            return View(shipment.ToList());
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/Create
        public ActionResult Create()
        {
            ViewBag.ServiceType = new SelectList(db.ServiceTypes, "Type", "Type");
            ViewBag.Destination = new SelectList(db.Destinations, "City", "City");
            ViewBag.Origin = new SelectList(db.Destinations, "City", "City");
            ViewBag.PayToFee = new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }
                    
                    };
            ViewBag.PayToDuties = new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }

                    };
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WaybillId,ReferenceNumber,ServiceType,ShippedDate,DeliveredDate,RecipientName,NumberOfPackages,Origin,Destination,Status,ShippingAccountId,RecipientPhoneNumber,DeliveryAddress,NotificationToSender,NotificationToRecipient")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.ShippedDate = new DateTime(2017, 05, 11);
                shipment.DeliveredDate = new DateTime(2017, 05, 11);

                shipment.Status = "Created";
                shipment.ShippingAccountId = db.ShippingAccounts.Where(s => s.UserName == User.Identity.Name).Select(s => s.ShippingAccountId).Single();


                shipment.NumberOfPackages = 0;
                db.Shipments.Add(shipment);
                db.SaveChanges();
                Session["WaybillId"] = shipment.WaybillId;
                
                return RedirectToAction("Create", "Packages");
            }
            
           // ViewBag.ServiceTypeID  = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", shipment.ServiceTypeID);
            return View(shipment);
        }
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            shipment.Status = "Confirmed";
            Session["WaybillId"] = id;
            db.Entry(shipment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Create","PickupInforations");
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceType = new SelectList(db.ServiceTypes, "Type", "Type");
            ViewBag.Destination = new SelectList(db.Destinations, "City", "City");
            ViewBag.Origin = new SelectList(db.Destinations, "City", "City");
            ViewBag.PayToFee = new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }

                    };
            ViewBag.PayToDuties = new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }

                    };
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WaybillId,ReferenceNumber,ServiceType,ShippedDate,DeliveredDate,RecipientName,NumberOfPackages,Origin,Destination,Status,ShippingAccountId")] Shipment shipment)
        {
            if (ModelState.IsValid)
  
            {
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceType= new SelectList(db.ServiceTypes, "Type", "Type", shipment.ServiceType);
            ViewBag.Destination = new SelectList(db.Destinations, "City", "City",shipment.Destination);
            ViewBag.Origin = new SelectList(db.Destinations, "City", "City",shipment.Origin);
            ViewBag.PayToFee = new SelectList(new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }

                    }, shipment.PayToFee);
            ViewBag.PayToDuties = new SelectList(new List<SelectListItem> {
                       new SelectListItem { Value = "Sender" , Text = "Sender" },
                       new SelectListItem { Value = "Recipient" , Text = "Recipient" }

                    }, shipment.PayToDuties);
            Session["WaybillId"] = shipment.WaybillId;
            return RedirectToAction("Index","Packages");
        }
        public ActionResult AddWeight(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            Session["WaybillId"] = shipment.WaybillId;
            return RedirectToAction("Index","Packages");
        }

        // POST: Shipments/Delete/5
        

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            db.Shipments.Remove(shipment);
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

        /* lab06 part 2
        //GET: Shipments/GetShipmentsRecord/
        public ActionResult GetShippingAccountRecord() {
            //Get the user name of the currently logged in user.
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (userName == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ShippingAccount shippingAccount = db.ShippingAccounts.SingleOrDefault( a => a.UserName == userName);
            if (shippingAccount == null) {
                return HttpNotFound("There is no s with user name \"" + userName + "\".");
            }
        }
        */

        

    }
}
