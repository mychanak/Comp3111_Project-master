using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        public virtual int PackageId { get; set; }
        public virtual int PackageTypeID { get; set; }
        public virtual PackageType PackageType { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Value { get; set; }
        public virtual decimal Weight { get; set; }
        [ForeignKey("Shipment")]
        public virtual int WaybillId { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}