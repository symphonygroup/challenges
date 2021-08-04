using System;

namespace CongestionTaxCalculator.Domain
{
    public class SwedenPublicHoliday
    {
        public static bool IsPublicHoliday(DateTime datetime)
        {
            var year = datetime.Year;
            var date = datetime.Date;

            switch (datetime.Month)
            {
                case 1:
                    if (NewYear(year) == date)
                        return true;
                    if (Epiphany(year) == date)
                        return true;
                    break;
                case 3:
                case 4:
                    if (GoodFriday(year) == date)
                        return true;
                    if (GetEaster(year) == date)
                        return true;
                    if (EasterMonday(year) == date)
                        return true;
                    if (Ascension(year) == date)
                        return true;
                    break;
                case 5:
                    if (LabourDay(year) == date)
                        return true;
                    if (Ascension(year) == date)
                        return true;
                    if (WhitSunday(year) == date)
                        return true;
                    break;
                case 6:
                    if (Ascension(year) == date)
                        return true;
                    if (WhitSunday(year) == date)
                        return true;
                    if (NationalDay(year) == date)
                        return true;
                    if (MidsummerDay(year) == date)
                        return true;
                    break;
                case 10:
                case 11:
                    if (AllSaintsDay(year) == date)
                        return true;
                    break;
                case 12:
                    if (Christmas(year) == date)
                        return true;
                    if (BoxingDay(year) == date)
                        return true;
                    break;
            }

            return false;
        }

        private static DateTime NewYear(int year)
        {
            return new DateTime(year, 1, 1);
        }
        private static DateTime Epiphany(int year)
        {
            return new DateTime(year, 1, 6);
        }
        private static DateTime GoodFriday(int year)
        {
            var hol = GetEaster(year);
            hol = hol.AddDays(-2);
            return hol;
        }
        private static DateTime EasterMonday(int year)
        {
            var hol = GetEaster(year);
            hol = hol.AddDays(1);
            return hol;
        }

        private static DateTime GetEaster(int year)
        {
            //should be
            //Easter Monday  28 Mar 2005  17 Apr 2006  9 Apr 2007  24 Mar 2008

            //Oudin's Algorithm - http://www.smart.net/~mmontes/oudin.html
            var g = year % 19;
            var c = year / 100;
            var h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
            var i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));
            var j = (year + year / 4 + i + 2 - c + c / 4) % 7;
            var p = i - j;
            var easterDay = 1 + (p + 27 + (p + 6) / 40) % 31;
            var easterMonth = 3 + (p + 26) / 30;

            return new DateTime(year, easterMonth, easterDay);
        }

        private static DateTime LabourDay(int year)
        {
            return new DateTime(year, 5, 1);
        }
        private static DateTime Ascension(int year)
        {
            var hol = GetEaster(year);
            hol = hol.AddDays(39);
            return hol;
        }
        private static DateTime NationalDay(int year)
        {
            return new DateTime(year, 6, 6);
        }
        private static DateTime MidsummerDay(int year)
        {
            DateTime dt = new DateTime(year, 6, 20);
            for (int i = 0; i < 7; i++)
            {
                if (dt.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                    return dt.AddDays(i);
            }
            return DateTime.MinValue;
        }

        private static DateTime WhitSunday(int year)
        {
            var hol = GetEaster(year);
            hol = hol.AddDays(7 * 7);
            return hol;
        }

        private static DateTime AllSaintsDay(int year)
        {
            DateTime dt = new DateTime(year, 10, 31);
            for (int i = 0; i < 7; i++)
            {
                if (dt.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                    return dt.AddDays(i);
            }
            return DateTime.MinValue;
        }
        private static DateTime Christmas(int year)
        {
            return new DateTime(year, 12, 25);
        }
        private static DateTime BoxingDay(int year)
        {
            return new DateTime(year, 12, 26);
        }
    }
}