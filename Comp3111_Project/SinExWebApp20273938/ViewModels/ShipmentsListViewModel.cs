﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.ViewModels
{
    [Table("ShipmentsListViewModel")]
    public class ShipmentsListViewModel
    {
        public virtual int WaybillId { get; set; }
        //public virtual string ReferenceNumber { get; set; }    
        public virtual string ServiceType { get; set; }
        public virtual DateTime ShippedDate { get; set; }
        public virtual DateTime DeliveredDate { get; set; }
        public virtual string RecipientName { get; set; }
        public virtual int NumberOfPackages { get; set; }
        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        //public virtual string Status { get; set; }           
        public virtual int ShippingAccountId { get; set; }
    }
}