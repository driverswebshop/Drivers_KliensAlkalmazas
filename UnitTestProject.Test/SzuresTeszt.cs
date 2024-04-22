using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
//using Drivers_KliensAlkalamazas.Controllers;
using System.Activities;
using Drivers_KliensAlkalamazas;
using Drivers_KliensAlkalamazas.Services;
using Drivers_KliensAlkalamazas.Controllers;

namespace UnitTestProject.Test
{
    public class SzuresTeszt
    {
        [
            Test,
            TestCase("H1234567",false),
            TestCase("G1234567",true),
            TestCase("T12345678",false),
            TestCase("T1234567",true)
            ]
        public void SKUValidator(string sku, bool expectedResult)
        {
            //Arrange
            var szuresController = new SzuresController();

            //Act
            var actualResult = szuresController.SzuresValidator(sku);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
