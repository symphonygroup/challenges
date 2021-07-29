using System;
using System.Collections.Generic;

namespace congestion.calculator
{
    public interface ITaxCalculator
    {
        int GetTax(Vehicle vehicle, IEnumerable<DateTime> dates);
    }
}