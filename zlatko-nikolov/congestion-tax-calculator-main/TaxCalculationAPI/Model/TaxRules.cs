using System;
using System.Collections.Generic;
using System.IO;
using TaxCalculationAPI.Interfaces;

namespace TaxCalculationAPI.Models
{
    public class TaxRules : ITaxRules
    {

        /// <summary>
        /// Opens a .txt document with rules
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetTaxRules(string city)
        {
            Dictionary<string, string> taxRulesDictionary = new Dictionary<string, string>();

            using (var sr = new StreamReader($"{city}.txt"))
            {
                string line = null;

                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    taxRulesDictionary.Add(values[0], values[1]);
                }
            }

            return taxRulesDictionary;
        }
    }
}
