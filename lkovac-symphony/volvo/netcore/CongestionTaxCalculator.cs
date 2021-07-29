using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
using congestion.calculator.Rule;
using congestion.calculator.TollFee;

public class CongestionTaxCalculator : ITaxCalculator
{
    private readonly int _maxTaxPerDay;
    private readonly ICompareDateRules _rules;
    private readonly double _singleRuleCheckInSeconds;
    private readonly ITollFeeProvider _tollFeeProvider;
    private readonly ITollFreeVehicleProvider _tollFreeVehicleProvider;

    public CongestionTaxCalculator(ICompareDateRules rules,
        ITollFeeProvider tollFeeProvider,
        ITollFreeVehicleProvider tollFreeVehicleProvider,
        int maxTaxPerDay,
        double singleRuleCheckInSeconds)
    {
        _rules = rules;
        _tollFeeProvider = tollFeeProvider ?? throw new ArgumentNullException(nameof(tollFeeProvider));
        _tollFreeVehicleProvider = tollFreeVehicleProvider ?? throw new ArgumentNullException(nameof(tollFreeVehicleProvider));
        _maxTaxPerDay = maxTaxPerDay;
        _singleRuleCheckInSeconds = singleRuleCheckInSeconds;
    }

    public int GetTax(Vehicle vehicle, IEnumerable<DateTime> dates) =>
        _tollFreeVehicleProvider.IsTollFreeVehicle(vehicle)
            ? 0
            : dates.GroupBy(x => x.Date)
                .Select(GetTaxPerDay)
                .Sum();

    public int GetTaxPerDay(IEnumerable<DateTime> dates)
    {
        if (dates == null || !dates.Any()) return 0;

        var validDates = dates
            .Where(x => _rules.Check(x))
            .OrderBy(x => x)
            .ToArray();

        if (validDates.Length == 0) return 0;

        if (validDates.Length == 1) return _tollFeeProvider.GetTollFee(validDates[0]);

        DateTime intervalStart = validDates[0];
        int currentHighestFeeInInterval = _tollFeeProvider.GetTollFee(validDates[0]);
        int totalFee = currentHighestFeeInInterval;

        foreach (var date in validDates.Skip(1))
        {
            var currentFee = _tollFeeProvider.GetTollFee(date);
            
            // 60 minutes sliding window for single charge rule
            if ((date - intervalStart).TotalSeconds <= _singleRuleCheckInSeconds)
            {
                // if current fee is not higher than previous, ignore it.
                if (currentFee <= currentHighestFeeInInterval) continue;

                // exclude previous current highest fee from total, add new one
                totalFee -= currentHighestFeeInInterval;
            }
            else
            {
                // add fee to total fee, reset buffer and set new currentHighestFee and interval start
                intervalStart = date;
            }

            currentHighestFeeInInterval = currentFee;
            totalFee += currentFee;
        }

        return totalFee > _maxTaxPerDay
            ? _maxTaxPerDay
            : totalFee;
    }
}