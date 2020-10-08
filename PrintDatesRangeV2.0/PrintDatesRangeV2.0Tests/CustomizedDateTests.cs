using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintDatesRange2._0;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintDatesRange2._0.Tests
{
    [TestClass()]
    public class CustomizedDateTests
    {
        [TestMethod()]
        public void GetFullDatePL()
        {
            var culture = CultureInfo.GetCultureInfo("pl-PL");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("28.12.1209");

            string resultDate = date.GetDateToPrint();

            Assert.AreEqual("28.12.1209", resultDate);
        }

        [TestMethod()]
        public void GetFullDateUS()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("5/30/1209");

            string resultDate = date.GetDateToPrint();

            Assert.AreEqual("5/30/1209", resultDate);
        }

        [TestMethod()]
        public void GetFullDateSE()
        {
            var culture = CultureInfo.GetCultureInfo("se-SE");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("1209-03-24");

            string resultDate = date.GetDateToPrint();

            Assert.AreEqual("1209-03-24", resultDate);
        }

        [TestMethod()]
        public void GetDateWithNoYearPL()
        {
            var culture = CultureInfo.GetCultureInfo("pl-PL");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("28.12.1209");

            string resultDate = date.GetDateToPrint(false, true);

            Assert.AreEqual("28.12", resultDate);
        }

        [TestMethod()]
        public void GetDateWithNoYearSE()
        {
            var culture = CultureInfo.GetCultureInfo("se-SE");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("1209-03-24");

            string resultDate = date.GetDateToPrint(false, true);

            Assert.AreEqual("03-24", resultDate);
        }

        [TestMethod()]
        public void GetDayOnlyPL()
        {
            var culture = CultureInfo.GetCultureInfo("pl-PL");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("28.12.1209");

            string resultDate = date.GetDateToPrint(false, false);

            Assert.AreEqual("28", resultDate);
        }

        [TestMethod()]
        public void GetDayOnlyUS()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("5/30/1209");

            string resultDate = date.GetDateToPrint(false, false);

            Assert.AreEqual("30", resultDate);
        }

        [TestMethod()]
        public void GetDayOnlySE()
        {
            var culture = CultureInfo.GetCultureInfo("se-SE");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CustomizedDate date = new CustomizedDate("1209-03-24");

            string resultDate = date.GetDateToPrint(false, false);

            Assert.AreEqual("24", resultDate);
        }
    }
}