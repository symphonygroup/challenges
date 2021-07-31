using System;
using System.Collections.Generic;

namespace TaxCalculationAPI.Interfaces
{
    public interface ITaxRules
    {
        IDictionary<string,string> GetTaxRules(string city);
    }
}
