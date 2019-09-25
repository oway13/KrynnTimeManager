using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrynnTimeManager.lib
{
    [Serializable]
    struct KrynnDateTime
    {
        public KrynnDateTime(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Minute = 0;
            this.Hour = 0;
            this.Second = 0;
            this.DayOfWeek = this.Day % 7;
        }

        public static readonly KrynnDateTime MinValue;
        public int Year { get; }
        public int Month { get; }
        public int Minute { get; }
        public int Hour { get; }
        public int Day { get; }
        public int Second { get; }
        public int DayOfWeek { get; }

        public string toDoubleDigit(int toConvert)
        {
            if (toConvert.ToString().Length == 1)
                return "0" + toConvert.ToString();
            else
                return toConvert.ToString();
        }

        public override string ToString()
        {
            string retString = "";
            //Date
            retString += KrynnDateTimeNames.getDayOfWeek(this.DayOfWeek) + ", ";
            retString += KrynnDateTimeNames.getMonth(this.Month) + " ";
            retString += this.Day.ToString() + ", ";
            retString += this.Year.ToString() + " AC. Time: ";

            //Time
            retString += toDoubleDigit(this.Hour)+":";
            retString += toDoubleDigit(this.Minute) + ":";
            retString += toDoubleDigit(this.Second) + " ";

            return retString;
        }
    }
}
