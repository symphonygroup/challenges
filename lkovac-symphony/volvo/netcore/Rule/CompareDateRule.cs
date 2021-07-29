using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion.calculator.Rule
{
    public interface ICompareDateRules
    {
        bool Check(DateTime dateTime);
    }

    /// <summary>
    ///     This class wraps domain rules for checking if tax fee should be included or not based on datetime.
    /// </summary>
    public class CompareDateRules : ICompareDateRules
    {
        private readonly IEnumerable<Predicate<DateTime>> _dateComparePredicates;

        public CompareDateRules(IEnumerable<Predicate<DateTime>> dateComparePredicates)
        {
            _dateComparePredicates = dateComparePredicates;
        }

        /// <summary>
        /// </summary>
        /// <param name="dateTime">Date to evaluate</param>
        /// <returns>Aggregated OR between rule checks - if at least one rule returned true it means that fee should be excluded</returns>
        public bool Check(DateTime dateTime) => !_dateComparePredicates.Any(x => x(dateTime));
    }
}