using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxCalculator.Api.Configuration
{
    public class TaxConfiguration
    {
        public int[] OnMonths { get; set; }

        public int[] OnSpecificDays { get; set; }

        public string[] OnHolidays { get; set; }

        public int SingleChargeRuleInSeconds { get; set; }

        public int MaxTaxPerDay { get; set; }

        public IEnumerable<DateTime> GetParsedHolidays() =>
            OnHolidays.Select(x =>
            {
                var date = x.Split("-").Select(int.Parse);
                return new DateTime(2013, date.First(), date.Last());
            });

        public IEnumerable<DayOfWeek> GetSpecificDays() => OnSpecificDays.Select(x => (DayOfWeek) x);
    }
}