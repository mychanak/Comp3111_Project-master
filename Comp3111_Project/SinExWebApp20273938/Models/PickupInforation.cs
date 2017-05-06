using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("PickupInformation")]
    public class PickupInforation
    {
        [Key]
        [ForeignKey("Shipment")]
        public virtual int WaybillId { get; set; }
        public string Pickuptype { get; set; }
        public string Pickuplocation { get; set; }
        public DateTime PickupDateTime { get; set; }
        public Shipment Shipment { get; set; }
    }
}