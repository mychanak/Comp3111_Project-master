using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.WebPages.Html; we are using MVC framework!

namespace SinExWebApp20273938.ViewModels
{
    //[Table("ShipmentSearchViewModel")]
    public class ShipmentsSearchViewModel
    {
        public virtual int ShippingAccountId { get; set; }

        public virtual DateTime FromShippedDate { get; set; }

        public virtual DateTime FromDeliveredDate { get; set; }

        public virtual List<SelectListItem> ShippingAccounts { get; set; }
    }
}