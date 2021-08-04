using System;
using System.Collections.Generic;
using System.Linq;

namespace CongestionTaxCalculator.Domain
{
    public class Calculator
    {
        private readonly CalculatorConfiguration _configuration;

        public Calculator(CalculatorConfiguration configuration)
        {
            _configuration = configuration;
        }
        /**
             * Calculate the total toll fee for one day
             *
             * @param vehicle - the vehicle
             * @param tollPasses   - date and time of all passes on one day
             * @return - the total congestion tax
             */

        public decimal GetTax(Vehicle vehicle, DateTime[] tollPasses)
        {
            if (IsTaxExempt(vehicle)) return 0;

            decimal totalFee = 0;

            var tollPassesSplitToDays = tollPasses.GroupBy(x => x.Date);

            foreach (var dailyTollPasses in tollPassesSplitToDays)
            {
                totalFee += GetDailyTax(vehicle, dailyTollPasses.ToArray());
            }

            return totalFee;
        }

        private decimal GetDailyTax(Vehicle vehicle, DateTime[] tollPasses)
        {
            tollPasses = tollPasses.OrderBy(x => x).ToArray();

            decimal dailyFee = 0;

            while (tollPasses.Any())
            {
                DateTime singleChargeIntervalStart = tollPasses[0];
                DateTime[] tollPassesWithinAnHour = GetTollPassesWithinAnHour(singleChargeIntervalStart, tollPasses);
                dailyFee += _configuration.SingleChargeRule ?
                    tollPassesWithinAnHour.Max(GetTollFee) :
                    tollPassesWithinAnHour.Sum(GetTollFee);

                tollPasses = tollPasses.Where(pass => !tollPassesWithinAnHour.Contains(pass)).ToArray();

                if (dailyFee >= _configuration.MaxDailyCharge) return _configuration.MaxDailyCharge;
            }

            return dailyFee;
        }

        public bool IsTaxExempt(Vehicle vehicle)
        {
            return _configuration.TaxExemptVehicles.Contains(vehicle.Type);
        }

        private DateTime[] GetTollPassesWithinAnHour(DateTime startTime, DateTime[] tollPasses)
        {
            return tollPasses
                .Where(pass =>
                    pass - startTime >= TimeSpan.FromMinutes(0) &&
                    pass - startTime < TimeSpan.FromMinutes(60))
                .ToArray();
        }

        private decimal GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;
            foreach (var fixedHourCharge in _configuration.FixedHourCharges)
            {
                if (date.TimeOfDay >= fixedHourCharge.FromTime && date.TimeOfDay <= fixedHourCharge.ToTime)
                {
                    return fixedHourCharge.Amount;
                }
            }

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (_configuration.TaxExemptWeekDays.Contains(date.DayOfWeek)) return true;

            if (_configuration.TaxExemptMonths.Contains(date.Month)) return true;

            if (_configuration.TaxExemptPublicHolidays
                && SwedenPublicHoliday.IsPublicHoliday(date)) return true;
            if (_configuration.TaxExemptDayBeforePublicHoliday
                && SwedenPublicHoliday.IsPublicHoliday(date.Add(TimeSpan.FromDays(1)))) return true;

            return false;
        }
    }
}