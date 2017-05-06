using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using X.PagedList;

namespace SinExWebApp20273938.ViewModels
{
    [Table("ShipmentReportViewModel")]
    public class ShipmentsReportViewModel
    {
        public virtual ShipmentsSearchViewModel Shipment { get; set; }
        public virtual IPagedList<ShipmentsListViewModel> Shipments { get; set; }
    }
}