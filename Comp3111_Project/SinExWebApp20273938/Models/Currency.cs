using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("Currency")]
    public class Currency
    {

        //public virtual string CurrencyID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string CurrencyCode { get; set; }

        public virtual decimal ExchangeRate { get; set; }
        //Navigation property to Destination.
        public virtual ICollection<Destination> Destinations { get; set; }

    }
}
