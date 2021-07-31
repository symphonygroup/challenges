using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using congestion_tax_calculator_main.Enums;
using congestion_tax_calculator_main.Interfaces;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */


    public int GetTax(IVehicle vehicle, DateTime[] dates, int maxFee, IDictionary<string,string> rules)
    {
        if (IsTollFreeVehicle(vehicle)) return 0;

        var rulesDictionary = rules;
        DateTime intervalStart = dates[0];
        int currentDay = dates[0].Day;
        int totalFeeCurrentDay = 0;
        int totalFee = 0;

        for (int i = 0; i < dates.Length; i++)
        {
            if (dates[i].Day != currentDay)
            {
                totalFeeCurrentDay = GetMaxFee(totalFeeCurrentDay, maxFee);
                totalFee += totalFeeCurrentDay;
                currentDay = dates[i].Day;
                totalFeeCurrentDay = 0;
            }

            int nextFee = GetTollFee(dates[i], vehicle, rulesDictionary);
            int tempFee = GetTollFee(intervalStart, vehicle, rulesDictionary);

            double minutes = (dates[i] - intervalStart).TotalMinutes;

            if (minutes <= maxFee)
            {
                totalFeeCurrentDay = ReduceTotalFeePerDay(totalFeeCurrentDay, tempFee);
                SetTemporaryFee(ref tempFee, ref nextFee);
                totalFeeCurrentDay += tempFee;
            }
            else
            {
                intervalStart = dates[i];
                totalFeeCurrentDay += nextFee;
            }
            
        }
        totalFee += GetMaxFee(totalFeeCurrentDay, maxFee);
        return totalFee;
    }

    private static void SetTemporaryFee(ref int tempFee, ref int nextFee)
    {
        if (nextFee >= tempFee) tempFee = nextFee;
    }

    private static int ReduceTotalFeePerDay(int totalFeeCurrentDay, int tempFee)
    {
        if (totalFeeCurrentDay > 0) totalFeeCurrentDay -= tempFee;
        return totalFeeCurrentDay;
    }

    public int GetMaxFee(int totalFee, int maxFee)
    {
        return (totalFee > maxFee) ? maxFee : totalFee;
    }

    public bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    //public static int GetTollFee(DateTime date, Vehicle vehicle)
    //{
    //    if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

    //    int hour = date.Hour;
    //    int minute = date.Minute;

    //    if (hour == 6 && minute >= 0 && minute <= 29) return 8;
    //    else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
    //    else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
    //    else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
    //    else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
    //    else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
    //    else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
    //    else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
    //    else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
    //    else return 0;
    //}

    //public int GetTollFee(DateTime date, Vehicle vehicle)
    //{
    //    if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

    //    int hour = date.Hour;
    //    int minute = date.Minute;

    //    switch (hour)
    //    {
    //        case 6: return (minute >= 0 && minute <= 29) ? 8 : 13;
    //        case 7: return 18;
    //        case 8: return (minute >= 0 && minute <= 29) ? 13 : 8;
    //        case 9:
    //        case 10:
    //        case 11:
    //        case 12:
    //        case 13:
    //        case 14: return 8;
    //        case 15: return (minute >= 0 && minute <= 29) ? 13 : 18;
    //        case 16: return 18;
    //        case 17: return 13;
    //        case 18: return (minute >= 0 && minute <= 29) ? 8 : 0;

    //        default: return 0;
    //    }
    //}

    public int GetTollFee(DateTime date, IVehicle vehicle, IDictionary<string, string> rulesDictionary)
    {
        
        foreach (KeyValuePair<string, string> entry in rulesDictionary)
        {
            var interval = entry.Key.Split('-');
            DateTime from = DateTime.ParseExact(interval[0], "HH:mm", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(interval[1], "HH:mm", CultureInfo.InvariantCulture);
            var currentDataTime = date.TimeOfDay;
            if (TimeSpan.Compare(from.TimeOfDay, currentDataTime) <= 0 && TimeSpan.Compare(to.TimeOfDay, currentDataTime) >= 0)
            {
                return int.Parse(entry.Value);
            }

        }

        return 0;
    }


    

    public bool IsTollFreeDate(DateTime date)
    {
        IsTollFreeDay(date.DayOfWeek);

        IsPublicHoliday(date);
        
        return false;
    }

    public bool IsTollFreeDay(DayOfWeek dayOfWeek)
    {
        return (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday);
    }

    public bool IsPublicHoliday(DateTime date)
    {
        int day = date.Day;
        int month = date.Month;

        if (date.Year == 2013)
        {
            return month switch
            {
                1 => day == 1,
                3 => (day == 28 || day == 29),
                4 => (day == 1 || day == 30),
                5 => (day == 1 || day == 8 || day == 9),
                6 => (day == 5 || day == 6 || day == 21),
                7 => true,
                11 => day == 1,
                12 => (day == 24 || day == 25 || day == 26 || day == 31),
                _ => false,
            };
        }

        return false;
    }
    
}