using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.ConfigurationStore;
using CongestionTaxCalculator.Domain;
using Xunit;

namespace CongestionTaxCalculator.Tests
{
    public class GothenburgTaxRulesTests
    {
        [Theory]
        [ClassData(typeof(GothenburgFixHourChargesData))]
        public void FixedHoursChargingInGothenburg(DateTime date, decimal amount)
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            var calculatedAmount = taxCalculator.GetTax(new Vehicle(VehicleType.Car), new[] { date });
            Assert.True(amount == calculatedAmount, $"Date: [{date}]");
        }

        
    }

    internal class GothenburgFixHourChargesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // 06:00–06:29	SEK 8
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(6, 0, 0),
                    TestData.ChargedDateForTime(6, 29, 59)
            ), 8};
            // 06:30–06:59	SEK 13
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(6, 30, 0),
                    TestData.ChargedDateForTime(6, 59, 59)
            ), 13};
            // 07:00–07:59	SEK 18
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(7, 0, 0),
                    TestData.ChargedDateForTime(7, 59, 59)
            ), 18};
            // 08:00–08:29	SEK 13
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(8, 0, 0),
                    TestData.ChargedDateForTime(8, 29, 59)
            ), 13};
            // 08:30–14:59	SEK 8
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(8, 30, 0),
                    TestData.ChargedDateForTime(14, 59, 59)
            ), 8};
            // 15:00–15:29	SEK 13
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(15, 0, 0),
                    TestData.ChargedDateForTime(15, 29, 59)
            ), 13};
            // 15:30–16:59	SEK 18
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(15, 30, 0),
                    TestData.ChargedDateForTime(16, 59, 59)
            ), 18};
            // 17:00–17:59	SEK 13
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(17, 0, 0),
                    TestData.ChargedDateForTime(17, 59, 59)
            ), 13};
            // 18:00–18:29	SEK 8
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(18, 0, 0),
                    TestData.ChargedDateForTime(18, 29, 59)
            ), 8};
            // 18:30–05:59	SEK 0
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(0, 0, 0),
                    TestData.ChargedDateForTime(5, 59, 59)
            ), 0};
            yield return new object[] {
                RandomDateTimeBetween(
                    TestData.ChargedDateForTime(18, 30, 0),
                    TestData.ChargedDateForTime(23, 29, 59)
            ), 0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private DateTime RandomDateTimeBetween(DateTime from, DateTime to)
        {
            var diff = (to - from).TotalSeconds;
            var secondsRandomizer = new Random(DateTime.Now.Millisecond);

            return from.AddSeconds(secondsRandomizer.Next(0, (int)diff));
        }
    }

    internal static class TestData
    {
        internal static DateTime ChargedDateForTime(int hour, int minute, int second)
        {
            return new DateTime(2021, 8, 4, hour, minute, second);
        }

        internal static CalculatorConfiguration GothenburgConfiguration => new ConfigurationReader().GetFor("Gothenburg");
    }
}
