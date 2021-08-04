using System;
using System.Linq;
using CongestionTaxCalculator.Domain;
using Xunit;

namespace CongestionTaxCalculator.Tests
{
    public class PublicHolidaysChargeRulesTests
    {
        [Fact]
        public void NoChargesOnWeekends()
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            var tollPasses = new[] {
                TestData.ChargedDateForTime(6,0,0), // 8
                new DateTime(2021, 8, 7, 6, 0, 0),  // 0 - Saturday
                new DateTime(2021, 8, 8, 6, 0, 0),  // 0 - Sunday
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(8, calculatedAmount);
        }

        [Fact]
        public void NoChargesOnWeekdays()
        {
            var config = TestData.GothenburgConfiguration;
            config.TaxExemptWeekDays = new [] {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
            var taxCalculator = new Calculator(config);
            var tollPasses = new[] {
                new DateTime(2021, 8, 2, 6, 0, 0),  // 8 - Mon
                new DateTime(2021, 8, 3, 6, 0, 0),  // 8 - Tue
                new DateTime(2021, 8, 4, 6, 0, 0),  // 8 - Wed
                new DateTime(2021, 8, 5, 6, 0, 0),  // 8 - Thu
                new DateTime(2021, 8, 6, 6, 0, 0),  // 8 - Fri
                new DateTime(2021, 8, 7, 6, 0, 0),  // 8 - Sat
                new DateTime(2021, 8, 8, 6, 0, 0),  // 8 - Sun
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(16, calculatedAmount);
        }
        
        [Fact]
        public void NoChargesOnPublicHolidays()
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            var tollPasses = new[] {
                TestData.ChargedDateForTime(6,0,0), // 8
            }.Concat(PublicHolidays()).ToArray();

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(8, calculatedAmount);
        }
        
        [Fact]
        public void ChargedOnPublicHolidays()
        {
            var config = TestData.GothenburgConfiguration;
            config.TaxExemptPublicHolidays = false;
            var taxCalculator = new Calculator(config);
            var tollPasses = new[] {
                TestData.ChargedDateForTime(6,0,0), // 8
                new DateTime(2021, 1, 1, 6, 0, 0),  //8
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(16, calculatedAmount);
        }

        [Fact]
        public void NoChargesOnDayBeforPublicHolidays()
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            var tollPasses = new[] {
                TestData.ChargedDateForTime(6,0,0), // 8
            }.Concat(
                PublicHolidays().Select(d => d.Subtract(TimeSpan.FromDays(1)))
            ).ToArray();

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(8, calculatedAmount);
        }

        [Fact]
        public void ChargedOnDayBeforPublicHolidays()
        {
            var config = TestData.GothenburgConfiguration;
            config.TaxExemptDayBeforePublicHoliday = false;
            var taxCalculator = new Calculator(config);
            var tollPasses = new[] {
                TestData.ChargedDateForTime(6, 0, 0),   // 8
                new DateTime(2020, 12, 31, 6, 0, 0),    // 8
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(16, calculatedAmount);
        }

        [Fact]
        public void NoChargesOnSpecificMonths()
        {
            var config = TestData.GothenburgConfiguration;
            config.TaxExemptMonths = new [] { 6, 7 };
            var taxCalculator = new Calculator(config);
            var tollPasses = new[] {
                new DateTime(2020, 6, 15, 6, 30, 0),    // 13
                new DateTime(2020, 7, 15, 6, 30, 0),    // 13
                new DateTime(2020, 8, 20, 6, 0, 0),    // 8
                new DateTime(2020, 9, 10, 6, 0, 0),    // 8
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(16, calculatedAmount);
        }

        private DateTime[] PublicHolidays()
        {
            return new[]{
                new DateTime(2021, 1, 1, 6, 0, 0),
                new DateTime(2022, 1, 1, 6, 0, 0),
                new DateTime(2021, 1, 6, 6, 0, 0),
                new DateTime(2021, 4, 2, 6, 0, 0),
                new DateTime(2021, 4, 5, 6, 0, 0),
                new DateTime(2019, 1, 1, 6, 0, 0),
                new DateTime(2021, 5, 13, 6, 0, 0),
                new DateTime(2019, 6, 6, 6, 0, 0),
                new DateTime(2019, 6, 6, 6, 0, 0),
                new DateTime(2021, 12, 25, 6, 0, 0),
                new DateTime(2021, 12, 26, 6, 0, 0)
            };
        }
    }

}
