using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//part 2 of Lab02
namespace SinExWebApp20273938.Models
{
    [Table("PackageTypeSize")]
    public class PackageTypeSize
    {
        [Key]
        public virtual int PackageTypeSizeID { get; set; }
        public virtual string SizeAndWeight { get; set; }
        public virtual int? WeightLimit { get; set; }

        // Foreign key references ServiceType.
        [ForeignKey("PackageType")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int PackageTypeID { get; set; }
        //Navigation property to PackageType.
        public virtual PackageType PackageType { get; set; }

        //public virtual ICollection<PackageType> PackageTypes { get; set; }
    }
}
