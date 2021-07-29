using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using congestion.calculator;
using congestion.calculator.Rule;
using congestion.calculator.TollFee;
using Xunit;

namespace TaxCalculator.UnitTest
{
    public class TaxCalculatorUnitTests
    {
        private const int MAX_TAX_FEE_PER_DAY = 60;
        private const int SCR_IN_SECONDS = 3600;

        private static readonly CompareDateRules CompareDateRules = new(new List<Predicate<DateTime>>
        {
            x => ParseToDates(HolidayDates)
                .Any(h => h.Date == x.Date || h.AddDays(-1).Date == x.Date),
            x => ParseToDates(RestrictedMonths).Any(m => x.Month == m.Month),
            x => RestrictedDayOfWeeks.Any(dof => dof == x.DayOfWeek)
        });

        private static readonly List<DayOfWeek> RestrictedDayOfWeeks = new()
        {
            DayOfWeek.Saturday, DayOfWeek.Sunday
        };

        private static readonly List<string> HolidayDates = new()
        {
            "2013-01-01 18:00:00",
            "2013-12-31 18:00:00",
        };

        private static readonly List<string> Weekends = new()
        {
            "2013-10-06 18:00:00",
            "2013-10-20 18:00:00"
        };

        private static readonly List<string> RestrictedMonths = new()
        {
            "2013-06-06 18:00:00",
            "2013-06-20 18:00:00"
        };

        private static readonly List<string> RegularDates = new()
        {
            "2013-01-14 06:59:00", // 13
            "2013-01-14 07:10:00", // 18 + 
            "2013-01-14 07:15:00", // 18
            "2013-10-11 08:31:00", // 8 +
            "2013-10-11 08:55:00", // 8
            "2013-10-11 10:00:00", // 8 +
            "2013-10-11 10:01:00", // 8
            "2013-10-11 10:02:00", // 8
            "2013-10-11 11:00:00", // 8
            "2013-10-11 17:00:00", // 13 +
            "2013-10-11 18:00:00"  // 8
        };

        private static readonly List<string> RegularDatesExceedLimit = new()
        {
            "2013-10-11 06:59:00",
            "2013-10-11 07:10:00",
            "2013-10-11 07:15:00",
            "2013-10-11 08:31:00",
            "2013-10-11 08:55:00",
            "2013-10-11 10:00:00",
            "2013-10-11 11:00:00",
            "2013-10-11 12:00:00",
            "2013-10-11 13:00:00",
            "2013-10-11 14:00:00",
            "2013-10-11 15:00:00",
            "2013-10-11 16:00:00",
            "2013-10-11 17:00:00",
            "2013-10-11 18:00:00"
        };

        private static readonly List<string> AssignmentDates = new()
        {
            "2013-01-14 21:00:00",
            "2013-01-15 21:00:00",
            "2013-02-07 06:23:27",
            "2013-02-07 15:27:00",
            "2013-02-08 06:27:00",
            "2013-02-08 06:20:27",
            "2013-02-08 14:35:00",
            "2013-02-08 15:29:00",
            "2013-02-08 15:47:00",
            "2013-02-08 16:01:00",
            "2013-02-08 16:48:00",
            "2013-02-08 17:49:00",
            "2013-02-08 18:29:00",
            "2013-02-08 18:35:00",
            "2013-03-26 14:25:00",
            "2013-03-28 14:07:27"
        };

        private static List<DateTime> ParseToDates(IEnumerable<string> dates) => dates.Select(DateTime.Parse).ToList();

        public static TheoryData<TestData> GetDates()
        {
            return new()
            {
                new TestData
                {
                    Dates = ParseToDates(RegularDates),
                    Expected = 47
                },
                new TestData
                {
                    Dates = ParseToDates(RegularDates)
                        .Union(ParseToDates(Weekends)),
                    Expected = 47
                },
                new TestData
                {
                    Dates = ParseToDates(RegularDates)
                        .Union(ParseToDates(Weekends))
                        .Union(ParseToDates(HolidayDates)),
                    Expected = 47
                },
                new TestData
                {
                    Dates = ParseToDates(RegularDates)
                        .Union(ParseToDates(Weekends))
                        .Union(ParseToDates(HolidayDates))
                        .Union(ParseToDates(RestrictedMonths)),
                    Expected = 47
                },
                new TestData
                {
                    Dates = ParseToDates(RegularDatesExceedLimit)
                        .Union(ParseToDates(Weekends))
                        .Union(ParseToDates(HolidayDates))
                        .Union(ParseToDates(RestrictedMonths)),
                    Expected = MAX_TAX_FEE_PER_DAY
                },
                new TestData
                {
                    Dates = ParseToDates(HolidayDates)
                        .Union(ParseToDates(HolidayDates).Select(x => x.AddDays(-1))),
                    Expected = 0
                },
                new TestData
                {
                    Dates = ParseToDates(AssignmentDates).ToList(),
                    Expected = 97
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetDates))]
        public async Task
            Given_Vehicle_Is_Car_When_There_Are_No_SpecialExemptDates_Tax_Should_Not_Be_Exempted_In_Respect_With_SCR(
                TestData data)
        {
            // Arrange
            var sut = new CongestionTaxCalculator(CompareDateRules, new TollFeeProvider(),
                new TollFreeVehicleProvider(), MAX_TAX_FEE_PER_DAY, SCR_IN_SECONDS);

            // Act
            var result = sut.GetTax(new Car(), data.Dates);

            // Assert
            Assert.Equal(data.Expected, result);
        }

        [Theory]
        [MemberData(nameof(GetDates))]
        public async Task
            Given_Vehicle_Is_Military_When_There_Are_No_SpecialExemptDates_Tax_Should_Be_Exempted_In_Respect_With_SCR(
                TestData data)
        {
            // Arrange
            var sut = new CongestionTaxCalculator(CompareDateRules, new TollFeeProvider(),
                new TollFreeVehicleProvider(), MAX_TAX_FEE_PER_DAY, SCR_IN_SECONDS);

            // Act
            var result = sut.GetTax(new Military(), data.Dates);

            // Assert
            Assert.Equal(0, result);
        }

        public class TestData
        {
            public IEnumerable<DateTime> Dates { get; init; }

            public int Expected { get; init; }
        }
    }
}