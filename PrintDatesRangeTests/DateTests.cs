using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintDatesRange;

namespace PrintDatesRange.Tests
{
    [TestClass()]
    public class DateTests
    {
        [TestMethod()]
        public void IsLeapYearTest()
        {
            Date firstLeapYearDate = new Date("29.02.1604");
            Date secondLeapYearDate = new Date("29.02.1600");
            Date firstNotLeapYearDate = new Date("28.02.1605");
            Date secondNotLeapYearDate = new Date("28.02.1700");

            bool leapResult = firstLeapYearDate.IsLeapYear() && secondLeapYearDate.IsLeapYear();
            bool notLeapResult = firstNotLeapYearDate.IsLeapYear() || secondNotLeapYearDate.IsLeapYear();
            bool result = leapResult && !notLeapResult;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsSameYearTest()
        {
            Date firstDateTrue = new Date("11.11.1918");
            Date secondDateTrue = new Date("08.01.1918");

            Date firstDateFalse = new Date("11.11.1918");
            Date secondDateFalse = new Date("11.11.1");

            bool isSameYearTrue = firstDateTrue.IsSameYear(firstDateTrue);
            bool isSameYearFalse = firstDateFalse.IsSameYear(secondDateFalse);
            bool result = isSameYearTrue && !isSameYearFalse;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsSameMonthTest()
        {
            Date firstDateTrue = new Date("03.11.1918");
            Date secondDateTrue = new Date("08.11.1919");

            Date firstDateFalse = new Date("09.11.875");
            Date secondDateFalse = new Date("09.01.875");

            bool isSameMonthTrue = firstDateTrue.IsSameMonth(secondDateTrue);
            bool isSameMonthFalse = firstDateFalse.IsSameMonth(secondDateFalse);
            bool result = isSameMonthTrue && !isSameMonthFalse;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsSameDayTest()
        {
            Date firstDateTrue = new Date("31.12.2019");
            Date secondDateTrue = new Date("31.12.2020");


            Date firstDateFalse = new Date("30.12.2020");
            Date secondDateFalse = new Date("31.12.2020");

            bool isSameDayTrue = firstDateTrue.IsSameDay(firstDateTrue);
            bool isSameDayFalse = firstDateFalse.IsSameDay(secondDateFalse);
            bool result = isSameDayTrue && !isSameDayFalse;

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetFullDateToPrintTest()
        {
            Date date = new Date("24.09.2020");
            string expected = "24.09.2020";

            string actual = date.GetDateToPrint(true, true).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetDateWithNoYearToPrintTest()
        {
            Date date = new Date("24.09.2020");
            string expected = "24.09";

            string actual = date.GetDateToPrint(false, true).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetOnlyDayToPrintTest()
        {
            Date date = new Date("24.09.2020");
            string expected = "24";

            string actual = date.GetDateToPrint(false, false).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NotDigitInputThrowsException()
        {
            string expectedException = "Provided date does not suite 'dd.mm.year' format.";
            Exception actualException = null;

            try
            {
                Date date = new Date("q1.a2.187b");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void SlashSplitFormatInputThrowsException()
        {
            string expectedException = "Provided date does not suite 'dd.mm.year' format.";
            Exception actualException = null;

            try
            {
                Date date = new Date("01/02/1873");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void ThreeDigitMonthInputThrowsException()
        {
            string expectedException = "Provided date does not suite 'dd.mm.year' format.";
            Exception actualException = null;

            try
            {
                Date date = new Date("01.012.1873");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void ThreeDigitDayInputThrowsException()
        {
            string expectedException = "Provided date does not suite 'dd.mm.year' format.";
            Exception actualException = null;

            try
            {
                Date date = new Date("001.12.1873");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void LongerStringInputThrowsException()
        {
            string expectedException = "Provided date does not suite 'dd.mm.year' format.";
            Exception actualException = null;

            try
            {
                Date date = new Date("02.03.1982.02");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }


        [TestMethod()]
        public void WrongYearInputThrowsException()
        {
            string expectedException = "Provided date does not exist. Check if year is correct.";
            Exception actualException = null;

            try
            {
                Date date = new Date("02.03.0");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void WrongMonthInputThrowsException()
        {
            string expectedException = "Provided date does not exist. Check if month is correct.";
            Exception actualException = null;

            try
            {
                Date date = new Date("02.13.1987");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void WrongDayInputThrowsException()
        {
            string expectedException = "Provided date does not exist. Check if day is correct.";
            Exception actualException = null;

            try
            {
                Date date = new Date("29.02.1605");
            }
            catch (Exception ex)
            {
                actualException = ex;
            }

            Assert.AreEqual(expectedException, actualException.Message);
        }

        [TestMethod()]
        public void CorrectDateInstanceCreation()
        {
            Exception exception = null;

            try
            {
                Date date = new Date("24.02.1999");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}