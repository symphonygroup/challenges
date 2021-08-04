using System;

namespace CongestionTaxCalculator.Domain
{
    public class CalculatorConfiguration
    {
        public bool SingleChargeRule { get; set; }
        public VehicleType[] TaxExemptVehicles { get; set; } = new VehicleType[0];
        public bool TaxExemptPublicHolidays { get; set; }
        public bool TaxExemptDayBeforePublicHoliday { get; set; }
        public int[] TaxExemptMonths { get; set; } = new int[0];
        public DayOfWeek[] TaxExemptWeekDays { get; set; } = new DayOfWeek[0];
        public decimal MaxDailyCharge { get; set; }
        public FixedHourCharge[] FixedHourCharges { get; set; } = new FixedHourCharge[0];
    }

    public class FixedHourCharge
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public decimal Amount { get; set; }

        public static FixedHourCharge New(TimeSpan fromTime, int lengthInMinutes, decimal amount)
        {
            return new FixedHourCharge
            {
                FromTime = fromTime,
                ToTime = fromTime + TimeSpan.FromMinutes(lengthInMinutes) - TimeSpan.FromSeconds(1),
                Amount = amount
            };
        }
    }
}