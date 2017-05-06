using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("Destination")]
    public class Destination
    {
        public virtual int DestinationID { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }

        public virtual Currency Currencies { get; set; }
        //public virtual ICollection<Currency> Currencies { get; set; }
    }
}
