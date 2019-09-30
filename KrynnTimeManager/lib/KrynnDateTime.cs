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
      if (this.Year > 421)
        this.ACYearPattern = ((Year - 421) % 3) + 1;
      else
        this.ACYearPattern = (Year % 3) + 1;
      normalizeTime();

    }

    public KrynnDateTime(int year, int month, int day, int hour, int minute, int second)
    {
      if (year < 421)
      {
        throw new ArgumentOutOfRangeException("year","Year cannot be before 421 AC");
      }
      this.Year = year;
      this.Month = month;
      this.Day = day;
      this.Minute = minute;
      this.Hour = hour;
      this.Second = second;
      this.DayOfWeek = this.Day % 7;
      if (this.Year > 421)
        this.ACYearPattern = ((Year - 421) % 3) + 1;
      else
        this.ACYearPattern = (Year % 3) + 1;
      normalizeTime();
    }

    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Minute { get; private set; }
    public int Hour { get; private set; }
    public int Day { get; private set; }
    public int Second { get; private set; }
    public int DayOfWeek { get; private set; }

    //In PDF, 421, 424, 427 = 3; 422,425,428 = 1; 423,426,429 = 2
    //In Code, 421, 424, 427 = 1; 422,425,428 = 2; 423,426,429 = 3
    public int ACYearPattern { get; private set; }

    private void normalizeTime()
    {
      while (Second > 60)
      {
        this.Second -= 60;
        this.Minute++;
      }

      while (Second < 0)
      {
        this.Second += 60;
        this.Minute--;
      }

      while (Minute > 60)
      {
        this.Minute -= 60;
        this.Hour++;
      }
      while (Minute < 0)
      {
        this.Minute += 60;
        this.Hour--;
      }

      while (Hour > 24)
      {
        this.Hour -= 24;
        this.Day++;
      }
      while (Hour < 0)
      {
        this.Hour += 24;
        this.Day--;
      }

      while (Day > 28)
      {
        this.Day -= 28;
        this.Month++;
      }
      while (Day < 1)
      {
        this.Day += 28;
        this.Month--;
      }

      while (Month > 12)
      {
        this.Month -= 12;
        this.Year++;
      }
      while (Month < 1)
      {
        this.Month += 12;
        this.Year--;
      }
      this.DayOfWeek = this.Day % 7;
      if (this.Year > 421)
        this.ACYearPattern = ((Year - 421) % 3) + 1;
      else
        this.ACYearPattern = (Year % 3) + 1;
    }

    public KrynnDateTime AddDays(int value)
    {
      return new KrynnDateTime(Year, Month, Day + value, Hour, Minute, Second);
    }
    public KrynnDateTime AddMonths(int value)
    {
      return new KrynnDateTime(Year, Month + value, Day, Hour, Minute, Second);
    }
    public KrynnDateTime AddYears(int value)
    {
      return new KrynnDateTime(Year + value, Month, Day, Hour, Minute, Second);
    }
    public KrynnDateTime AddSeconds(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour, Minute, Second + value);
    }
    public KrynnDateTime AddMinutes(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour, Minute + value, Second);
    }
    public KrynnDateTime AddHours(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour + value, Minute, Second);
    }

    public KrynnDateTime SubtractMonths(int value)
    {
      return new KrynnDateTime(Year, Month - value, Day, Hour, Minute, Second);
    }
    public KrynnDateTime SubtractDays(int value)
    {
      return new KrynnDateTime(Year, Month, Day - value, Hour, Minute, Second);
    }
    public KrynnDateTime SubtractYears(int value)
    {
      return new KrynnDateTime(Year - value, Month, Day, Hour, Minute, Second);
    }
    public KrynnDateTime SubtractSeconds(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour, Minute, Second - value);
    }
    public KrynnDateTime SubtractMinutes(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour, Minute - value, Second);
    }
    public KrynnDateTime SubtractHours(int value)
    {
      return new KrynnDateTime(Year, Month, Day, Hour - value, Minute, Second);
    }
    
    public int DaysSince(KrynnDateTime sinceDate)
    {
      int dateOneCount = 0;
      dateOneCount += sinceDate.Day;
      dateOneCount += sinceDate.Year * 336;
      dateOneCount += sinceDate.Month * 28;

      int dateTwoCount = 0;
      dateTwoCount += this.Day;
      dateTwoCount += this.Year * 336;
      dateTwoCount += this.Month * 28;
      return dateTwoCount - dateOneCount;
    }
    //TODO:Moooooooon Phases

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
      retString += toDoubleDigit(this.Hour) + ":";
      retString += toDoubleDigit(this.Minute) + ":";
      retString += toDoubleDigit(this.Second) + " ";

      return retString;
    }

    public string ToMonthYearString()
    {
      string retString = "";
      retString += KrynnDateTimeNames.getMonth(this.Month) + ", ";
      retString += this.Year.ToString() + " AC";
      return retString;
    }
  }
}
