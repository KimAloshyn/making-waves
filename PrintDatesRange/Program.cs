using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PrintDatesRange
{
    /// <summary>
    /// The Main Date class.
    /// It contsains some of the basic methods to work with dates.
    /// </summary>
    /// <remarks>
    /// This class can compare dates and check if years is a leap one.
    /// </remarks>
    public class Date
    {
        Dictionary<int, int> daysCountToMonthMapping = new Dictionary<int, int>()
        {
            {1, 31}, {2, 28}, {3, 31}, {4, 30}, {5, 31}, {6, 30},
            {7, 31}, {8, 31}, {9, 30}, {10, 31}, {11, 30}, {12, 31}
        };

        private int _day, _month, _year;

        public int Day
        {
            get
            { return _day; }
            set
            {
                if (IsLeapYear())
                    daysCountToMonthMapping[2] = 29;

                if ((Enumerable.Range(1, daysCountToMonthMapping[Month]).Contains(value)))
                    _day = value;
                else
                    throw new WrongDateInputException("Provided date does not exist. Check if day is correct.");
            }
        }

        public int Month
        {
            get
            { return _month; }
            set
            {
                if (Enumerable.Range(1, 12).Contains(value))
                    _month = value;
                else
                    throw new WrongDateInputException("Provided date does not exist. Check if month is correct.");
            }
        }

        public int Year
        {
            get
            { return _year; }
            set
            {
                if (value > 0)
                    _year = value;
                else
                    throw new WrongDateInputException("Provided date does not exist. Check if year is correct.");
            }
        }

        /// <summary>
        /// Constructor checks if given date suits the defined format. 
        /// Provided string is split into day, month and year, 
        /// which are parsed to Int32 and passed to properties. 
        /// </summary>
        /// <param name="date">A string which is supposed to be a date and provided by user.</param>
        /// <exception cref="WrongDateInputException">Thrown when parameter does not suits 
        /// the defined format.</exception>
        public Date(string date)
        {
            bool isRightFormat = Regex.IsMatch(date, @"^(\d{1,2}\.){2}\d{1,}$");

            if (!isRightFormat)
                throw new WrongDateInputException("Provided date does not suite 'dd.mm.year' format.");

            string[] dateArray = date.Split('.');

            this.Year = Int32.Parse(dateArray[2]);
            this.Month = Int32.Parse(dateArray[1]);
            this.Day = Int32.Parse(dateArray[0]);
        }

        /// <summary>
        /// Checks if year is a leap one.
        /// </summary>
        /// <returns>
        /// Returns the bool value telling if year is leap or not.
        /// </returns>
        public bool IsLeapYear()
        {
            if (this.Year % 4 == 0)
            {
                if (this.Year % 100 == 0)
                {
                    return (this.Year % 400 == 0);
                }
                else
                    return true;
            }
            else
                return false;
        }


        public bool IsSameYear(Date comparedDate)
        {
            return Year == comparedDate.Year;
        }

        public bool IsSameMonth(Date comparedDate)
        {
            return Month == comparedDate.Month;
        }

        public bool IsSameDay(Date comparedDate)
        {
            return Day == comparedDate.Day;
        }

        /// <summary>
        /// Generates a string with date. The format (dd or dd.mm or dd.mm.yyyy)
        /// depends on the parameters printYear and printMonth.
        /// </summary>
        /// <param name="printYear"> Parameter which tells if the string should contain the year.</param>
        /// <param name="printMonth">Parameter which tells if the string should contain the month.</param>
        /// <returns>The string with date.</returns>
        public string GetDateToPrint(bool printYear = true, bool printMonth = true)
        {
            StringBuilder dateToPrint = new StringBuilder();
            dateToPrint.AppendFormat("{0:00}", Day);

            if (printMonth)
                dateToPrint.AppendFormat(".{0:00}", Month);
            if (printYear)
                dateToPrint.AppendFormat(".{0}", Year);

            return dateToPrint.ToString(); ;
        }

        /// <summary>
        /// Operator 'less than' is overloaded.
        /// It does the comparison of two provided dates using properties (day, month and year) of provided objects.
        /// </summary>
        /// <param name="firstDate"> A Date object used for the comparison.</param>
        /// <param name="secondDate">A Date object used for the comparison.</param>
        /// <returns>A bool which tells if statement 'first date is less than second date" is true</returns>
        public static bool operator <(Date firstDate, Date secondDate)
        {
            if (firstDate.Year < secondDate.Year)
                return true;
            else if (firstDate.Year == secondDate.Year)
            {
                if (firstDate.Month < secondDate.Month)
                    return true;
                else if (firstDate.Month == secondDate.Month)
                {
                    return firstDate.Day < secondDate.Day;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //Summary: Same as for overloaded operator 'less then'.
        public static bool operator >(Date firstDate, Date secondDate)
        {
            if (firstDate.Year > secondDate.Year)
                return true;
            else if (firstDate.Year == secondDate.Year)
            {
                if (firstDate.Month > secondDate.Month)
                    return true;
                else if (firstDate.Month == secondDate.Month)
                {
                    return firstDate.Day > secondDate.Day;
                }
                else
                    return false;
            }
            else
                return false;
        }
    };

    [Serializable]
    public class WrongDateInputException : Exception
    {
        public WrongDateInputException() { }

        public WrongDateInputException(string message)
            : base(message) { }

        public WrongDateInputException(string message, Exception inner)
            : base(message, inner) { }

    }

    class Program
    {
        /// <summary>
        /// Main method takes 2 arguments from user (the execution is stopped when they are less or more than 2).
        /// Arguments are suposed to be a String type and represent two dates. Arguments are used to create 2 instances
        /// of Date class which are later compared. The result of comparison affects the order of dwo days which are printed at the end of program.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Input is invalid. You need to provide two arguments");
                Environment.Exit(1);
            }

            Date firstDate = null;
            Date secondDate = null;
            bool printYear = true, printMonth = true, printOneDateOnly = false;

            try
            {
                firstDate = new Date(args[0]);
                secondDate = new Date(args[1]);
            }
            catch (WrongDateInputException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            /*In this place we are checking if year, month and day are the same. Later it affects
             the format of first date printed (dd or dd.mm or dd.mm.yyyy)
             */
            if (firstDate.IsSameYear(secondDate))
            {
                printYear = false;
                if (firstDate.IsSameMonth(secondDate))
                {
                    printMonth = false;
                    printOneDateOnly = firstDate.IsSameDay(secondDate);
                }
            }

            if (printOneDateOnly)
                Console.WriteLine(firstDate.GetDateToPrint());
            else if (firstDate < secondDate)
                Console.WriteLine(firstDate.GetDateToPrint(printYear, printMonth) + " - " + secondDate.GetDateToPrint());
            else
                Console.WriteLine(secondDate.GetDateToPrint(printYear, printMonth) + " - " + firstDate.GetDateToPrint());
        }
    }
}