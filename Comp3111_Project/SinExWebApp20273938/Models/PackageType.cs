using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("PackageType")]
    public class PackageType
    {
        public virtual int PackageTypeID { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        //Navigation property to ServicePackageFee.
        public virtual ICollection<ServicePackageFee> ServicePackageFees { get; set; }

        //Navigation property to ServicePackageFee.
        public virtual ICollection<PackageTypeSize> PackageTypeSizes { get; set; }
    }
}