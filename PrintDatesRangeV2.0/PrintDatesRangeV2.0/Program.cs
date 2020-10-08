using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrintDatesRange2._0
{
    public class CustomizedDate
    {
        public DateTime Date { get; set; }

        public CustomizedDate(string dateInput)
        {
            if (DateTime.TryParse(dateInput, out DateTime _temp))
                Date = _temp;
            else
            {
                Console.WriteLine("The date {0} is invalid.", dateInput);
                Environment.Exit(1);
            }
        }

        public string GetDateToPrint(bool getYear = true, bool getMonth = true)
        {
            if (!getYear)
            {
                if (!getMonth)
                    return Date.ToString("dd");
                else
                    return Regex.Replace(Date.ToShortDateString(), @"[^\d]?" + Date.Year.ToString() + @"[^\d]?", "");
            }
            return Date.ToShortDateString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Input is invalid. You need to provide two arguments.");
                Environment.Exit(1);
            }

            CustomizedDate firstDate = new CustomizedDate(args[0]);
            CustomizedDate secondDate = new CustomizedDate(args[1]);

            var (printYear, printMonth, printOneDateOnly) = (true, true, false);

            if (firstDate.Date.Year == secondDate.Date.Year)
            {
                printYear = false;
                if (firstDate.Date.Month == secondDate.Date.Month)
                {
                    printMonth = false;
                    printOneDateOnly = (firstDate.Date.Day == secondDate.Date.Day);
                }
            }

            if (printOneDateOnly)
                Console.WriteLine(firstDate.GetDateToPrint());
            else if (firstDate.Date < secondDate.Date)
                Console.WriteLine(firstDate.GetDateToPrint(printYear, printMonth) + " - " + secondDate.GetDateToPrint());
            else
                Console.WriteLine(secondDate.GetDateToPrint(printYear, printMonth) + " - " + firstDate.GetDateToPrint());
        }
    }
}
