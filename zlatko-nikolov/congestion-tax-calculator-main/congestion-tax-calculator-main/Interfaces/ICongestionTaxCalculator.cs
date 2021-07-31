using System;
using System.Collections.Generic;

namespace congestion_tax_calculator_main.Interfaces
{
    public interface ICongestionTaxCalculator
    {
        int GetTax(IVehicle vehicle, DateTime[] dates, int maxFee, IDictionary<string, string> rules);
        int GetMaxFee(int totalFee, int maxFee);
        bool IsTollFreeVehicle(IVehicle vehicle);
        int GetTollFee(DateTime date, IVehicle vehicle, IDictionary<string, string> rulesDictionary);
        bool IsTollFreeDate(DateTime date);
        bool IsTollFreeDay(DayOfWeek dayOfWeek);
        bool IsPublicHoliday(DateTime date);
    }
}
