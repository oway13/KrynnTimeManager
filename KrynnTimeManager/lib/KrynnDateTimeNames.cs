using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrynnTimeManager.lib
{
    public class KrynnDateTimeNames
    {
        public static string getMonth(int month)
        {
            return ErgothianMonthsOfYear[month-1];
        }

        public static string getDayOfWeek(int dayOfWeek)
        {
            return ErgothianDaysOfWeek[dayOfWeek-1];
        }

        private static readonly String[] ErgothianMonthsOfYear = new string[]
        {
            "Aelmont",
            "Rannmont",
            "Mishamont",
            "Chislmont",
            "Bran",
            "Corij",
            "Argon",
            "Sirrimont",
            "Reorxmont",
            "Hiddumont",
            "H'rarmont",
            "Phoenix"
        };

        private static readonly String[] ErgothianDaysOfWeek = new string[] 
        {
            "Gileadai",
            "Luindai",
            "Nuindai",
            "Soldai",
            "Manthus",
            "Shinarai",
            "Boreadai",

        };
    }
}
