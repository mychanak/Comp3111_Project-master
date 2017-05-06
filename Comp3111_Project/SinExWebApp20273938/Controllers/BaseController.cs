using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20273938.Models;

namespace SinExWebApp20273938.Controllers
{
    public class BaseController : Controller
    {
        public decimal calculateFee(decimal originalFee, Decimal anotherFee) {
            return originalFee * anotherFee;
        }

        // GET: Base
        /*
        public ActionResult Index()
        {
            return View();
        }
        */
        private SinExDatabaseContext db = new SinExDatabaseContext();

        public decimal convertToCurrency(string currencyCode, decimal fee) {
            //If the currency exchange rates are not in Session state, load them into Session state.
            /*
            if (Session[currencyCode] == null)
            {
                var currencies = db.Currencies.ToList();
                foreach (var item in currencies)
                {
                    Session[item.CurrencyCode] = item.ExchangeRate;
                }
                //the following is also correct
                //db.Currencies.Where(s => s.CurrencyCode == currencyCode).Select(s => s.ExchangeRate).ToList();
                //Note that "ToList()" will return an ArrayList
            }
               //Session[currencyCode] != null
                return fee = fee * Decimal.Parse(Session[currencyCode].ToString());
                //the following is also correct
                //fee = fee * decimal.Parse(Session[currencyCode].ToString());

                //Note that "decimal" is a primitive type but "Decimal" is an object class type.
                // "fee = fee * (decimal) (Session[currencyCode]);" has no compilation error but it fails in runtime since direct conversion of an object to a primitive type is NOT allowed in C#.
             
             
             /* The following code are for testing only */
             /*
             var exchangeRate = 1.00M;
            switch(currencyCode){
                case "CNY":
                    exchangeRate = 1.00M;
                    break;
                case "HKD":
                    exchangeRate = 1.13M;
                    break;
                case "MOP":
                    exchangeRate = 1.16M;
                    break;
                case "TWD":
                    exchangeRate = 4.56M;
                    break;
            }
      
            return fee*exchangeRate;
            */
             // official solution
             //By default, only null value(s) will be inside Session[]
             
             if(Session[currencyCode] == null){
                var currencies = db.Currencies.ToList();
                foreach (var item in currencies){
                  Session[item.CurrencyCode] = item.ExchangeRate;    
                }
             }
             return fee* (Decimal)Session[currencyCode];
                
             

        }
    }
}
