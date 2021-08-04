using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.ConfigurationStore;
using CongestionTaxCalculator.Domain;
using Xunit;

namespace CongestionTaxCalculator.Tests
{
    public class MaxDailyChargeTests
    {
        [Theory]
        [ClassData(typeof(MaxDailyChargeData))]
        public void MaxDailyChargeIs(DateTime[] tollPasses, decimal maxDailyCharge)
        {
            var config = TestData.GothenburgConfiguration;
            config.MaxDailyCharge = maxDailyCharge;
            var taxCalculator = new Calculator(config);
            
            // additional 2 days
            tollPasses = tollPasses
                .Concat(tollPasses.Select(tp => tp.Subtract(TimeSpan.FromDays(1))))
                .Concat(tollPasses.Select(tp => tp.Subtract(TimeSpan.FromDays(2))))
                .ToArray();


            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(maxDailyCharge * 3, calculatedAmount);
        }
    }

    internal class MaxDailyChargeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // simple one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0), // 13
                    TestData.ChargedDateForTime(7, 19, 59) // 18
                }, 5};
            // unordered one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(7, 19, 59), // 18
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0) // 13
                }, 10};
            // multiple one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0), // 13
                    TestData.ChargedDateForTime(7, 19, 59),// 18
                    TestData.ChargedDateForTime(15, 0, 0), // 13
                    TestData.ChargedDateForTime(16, 28, 0),// 18
                    TestData.ChargedDateForTime(17, 23, 0),// 13
                    TestData.ChargedDateForTime(18, 35, 0),// 0
                }, 20};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
