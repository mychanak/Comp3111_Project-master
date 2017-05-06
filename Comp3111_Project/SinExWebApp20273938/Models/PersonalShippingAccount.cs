using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    //    [Table("ShippingAccount")]
    public class PersonalShippingAccount : ShippingAccount
    {
        [Required]
        [StringLength(35)]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(35)]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

    }
}
