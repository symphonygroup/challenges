using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.Domain;
using Xunit;

namespace CongestionTaxCalculator.Tests
{
    public class SingleChargeRuleTests
    {
        [Theory]
        [ClassData(typeof(SingleChargeData))]
        public void TollPassesWithinAnHourAreChargedOnlyOnceAsTheHighestOnesRate(DateTime[] tollPasses, decimal amount)
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            
            Assert.Equal(amount, taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses));
        }

        [Fact]
        public void WhenSingleChargeRuleIsDisabled_EveryTollPassIsBilled()
        {
            var config = TestData.GothenburgConfiguration;
            config.SingleChargeRule = false;
            var taxCalculator = new Calculator(config);
            
            // sums up to more than 60 for the day
            var tollPasses = new[]{
                TestData.ChargedDateForTime(6, 20, 0), // 8
                TestData.ChargedDateForTime(6, 58, 0), // 13
                TestData.ChargedDateForTime(7, 19, 59) // 18
            };

            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), tollPasses);

            Assert.Equal(39, calculatedAmount);
        }
    }

    internal class SingleChargeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // simple one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0), // 13
                    TestData.ChargedDateForTime(7, 19, 59) // 18 - this
                }, 18};
            // unordered one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(7, 19, 59), // 18 - this
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0) // 13
                }, 18};
            // multiple one hour time span
            yield return new object[] {
                new [] {
                    TestData.ChargedDateForTime(6, 20, 0), // 8
                    TestData.ChargedDateForTime(6, 58, 0), // 13
                    TestData.ChargedDateForTime(7, 19, 59),// 18 - this
                    TestData.ChargedDateForTime(15, 0, 0), // 13 - this
                    TestData.ChargedDateForTime(16, 28, 0),// 18 - this
                    TestData.ChargedDateForTime(17, 23, 0),// 13
                    TestData.ChargedDateForTime(18, 35, 0),// 0 - this
                }, 18 + 13 + 18 + 0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
