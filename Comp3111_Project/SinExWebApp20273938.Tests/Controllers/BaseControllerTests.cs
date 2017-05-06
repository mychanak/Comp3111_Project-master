using NUnit.Framework;
using SinExWebApp20273938.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SinExWebApp20273938.Models;

namespace SinExWebApp20273938.Controllers.Tests
{
    [TestFixture()]
    public class BaseControllerTests
    {
        [TestCase("CNY", 10, 10.0)]
        [TestCase("HKD", 10, 11.3)]
        [TestCase("MOP", 10, 11.6)]
        [TestCase("TWD", 10, 45.6)]
        //[Test()]
        public void convertToCurrencyTest(
           string ToCurreuncy, decimal fee, decimal resultfee
            )
        {
            BaseController b = new BaseController();
            ServicePackageFee testing = new ServicePackageFee();
            Assert.That(b.convertToCurrency(ToCurreuncy, fee), Is.EqualTo(resultfee));

        }
    }
}