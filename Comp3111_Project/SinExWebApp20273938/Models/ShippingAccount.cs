using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    [Table("ShippingAccount")]
    public abstract class ShippingAccount
    {
        public virtual int ShippingAccountId { get; set; }

        //Mailing Address Information

        [Required]
        [StringLength(50)]
        [Display(Name = "Building")]
        public virtual string Building { get; set; }

        [Required]
        [StringLength(35)]
        [Display(Name = "Street")]
        public virtual string Street { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "City")]
        public virtual string City { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Province")]
        public virtual string ProvinceCode { get; set; }

        [StringLength(6, MinimumLength = 5)]
        [Display(Name = "Postal Code")]
        public virtual string PostalCode { get; set; }
        //Q: (How) do we to check a valid country code or postal code that matches the convention for certain country or area specified or not?

        // Credit Card Information
        
        [Required(ErrorMessage = "The Type field is required")]
        [RegularExpression(@"^American Express|Diners Club|Discover|MasterCard|UnionPay|Visa$", ErrorMessage = "Only American Express, Diners Club, Discover, MasterCard, UnionPay and Visa should be accepted.")]
        [StringLength(16, MinimumLength = 4)]
        [Display(Name = "Type")]
        public virtual string Type { get; set; }
        //Q: (How) do we to check a valid choice of enumeration {“American Express”, “Diners Club”, “Discover”, “MasterCard”, “UnionPay”, “Visa”}?
        /*[Required(ErrorMessage = "The Type field is required")]
        [StringLength(16, MinimumLength = 4)]
        [Display(Name = "Type")]
        public virtual string Type {
            if(Type){
                get; set; } }
        */
        [Required]
        [StringLength(19, MinimumLength = 14)]
        [RegularExpression(@"^\d*$", ErrorMessage = "The field Number must be a number.")]
        [Display(Name = "Number")]
        public virtual string CardNumber { get; set; }
        //Q: (How) do we to check a valid choice of car number? Do we need to use Luhn algorithm?

        [StringLength(4, MinimumLength = 3)]
        [RegularExpression(@"^\d*$", ErrorMessage = "The field Security Number must be a number.")]
        [Display(Name = "Security Number")]
        public virtual string SecurityNumber { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "Cardholder Name")]
        public virtual string CardholderName { get; set; }

        [Required]
        [Range(1, 12)]
        [RegularExpression(@"^\d*$", ErrorMessage = "The field Expiry Month must be a number.")]
        [Display(Name = "Expiry Month")]
        public virtual string ExpiryMonth { get; set; }

        [Required]
        [Range(2017, 2047)]
        [RegularExpression(@"^\d*$", ErrorMessage = "The field Expiry Year must be a number.")]
        [Display(Name = "Expiry Year")]
        public virtual string ExpiryYear { get; set; }

        /*The following design option of string is also acceptable but using integer type allows faster validation
        [StringLength(2, MinimumLength = 1)]
        [RegularExpression(@"^[0 - 9] *$", ErrorMessage = "The field Expiry Month must be a number.")]
        [Display(Name = "Expiry Month")]
        public virtual string ExpiryMonth { get; set; }

        [StringLength(4, MinimumLength = 3)]
        [RegularExpression(@"^[0 - 9] *$", ErrorMessage = "The field Expiry Year must be a number.")]
        [Display(Name = "Expiry Year")]
        public virtual string ExpiryYear { get; set; }
        */

        [Required]
        [StringLength(14, MinimumLength = 8)]
        [RegularExpression(@"^\d*$", ErrorMessage = "Phone number must be numeric.")]
        [Display(Name = "Phonenumber")]
        public virtual string PhoneNumber { get; set; }

        [StringLength(30)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The Email field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public virtual string EmailAddress { get; set; }

        //[Required]
        [StringLength(12, MinimumLength = 12)]
        public virtual string AccountNumber { get; set; }

        //Lab06 part 1
        [StringLength(10)]
        public virtual string UserName { get; set; }

        //Navigation property to Shipment. 1 on the ShippingAccount side and N on the Shipment side
        public virtual ICollection<Shipment> Shipments { get; set; }

    }
}
