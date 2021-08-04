using System;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.ConfigurationStore
{
    // TODO: wire-up with actual data store
    public class ConfigurationReader
    {
        public CalculatorConfiguration GetFor(string city)
        {
            if (city == "Gothenburg")
            {
                return GothenburgConfiguration;
            }
            throw new InvalidOperationException($"City [{city}] is not supported.");
        }

        private CalculatorConfiguration GothenburgConfiguration =>
        new CalculatorConfiguration
        {
            SingleChargeRule = true,
            TaxExemptVehicles = new[] {
                    VehicleType.Emergency,
                    VehicleType.Bus,
                    VehicleType.Diplomat,
                    VehicleType.Motorcycle,
                    VehicleType.Military,
                    VehicleType.Foreign,
                },
            TaxExemptPublicHolidays = true,
            TaxExemptDayBeforePublicHoliday = true,
            TaxExemptMonths = new[] { 7 },
            TaxExemptWeekDays = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
            MaxDailyCharge = 60,
            FixedHourCharges = new[] {
                    FixedHourCharge.New(new TimeSpan(6,0,0), 30, 8),
                    FixedHourCharge.New(new TimeSpan(6,30,0), 30, 13),
                    FixedHourCharge.New(new TimeSpan(7,0,0), 60, 18),
                    FixedHourCharge.New(new TimeSpan(8,0,0), 30, 13),
                    new FixedHourCharge {FromTime = new TimeSpan(8, 30, 0), ToTime = new TimeSpan(14, 59, 59), Amount = 8},
                    FixedHourCharge.New(new TimeSpan(15,0,0),30, 13),
                    FixedHourCharge.New(new TimeSpan(15,30,0), 90, 18),
                    FixedHourCharge.New(new TimeSpan(17,0,0), 60, 13),
                    FixedHourCharge.New(new TimeSpan(18,0,0), 30, 8)
                }
        };
    }
}
