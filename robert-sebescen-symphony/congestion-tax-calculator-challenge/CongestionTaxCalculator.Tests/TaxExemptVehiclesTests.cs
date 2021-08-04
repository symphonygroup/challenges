using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.Domain;
using Xunit;

namespace CongestionTaxCalculator.Tests
{
    public class TaxExemptVehiclesTests
    {
        [Theory]
        [ClassData(typeof(ChargingBaseOnVehicleTypeData))]
        public void CharginBasedOnVehicleType(Vehicle vehicle, DateTime[] tollPasses, decimal amount)
        {
            var taxCalculator = new Calculator(TestData.GothenburgConfiguration);
            
            Assert.Equal(amount, taxCalculator.GetTax(vehicle, tollPasses));
        }
    }

    internal class ChargingBaseOnVehicleTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var tollPasses = new [] {
                    TestData.ChargedDateForTime(7, 19, 59) // 18
                };
            // Car should be charged
            yield return new object[] {new Vehicle(VehicleType.Car), tollPasses, 18};
            // Emergency vehicle should be charged
            yield return new object[] {new Vehicle(VehicleType.Emergency), tollPasses, 0};
            // Bus should be charged
            yield return new object[] {new Vehicle(VehicleType.Bus), tollPasses, 0};
            // Diplomat vehicle should be charged
            yield return new object[] {new Vehicle(VehicleType.Diplomat), tollPasses, 0};
            // Motorcycle should be charged
            yield return new object[] {new Vehicle(VehicleType.Motorcycle), tollPasses, 0};
            // Military vehicle should be charged
            yield return new object[] {new Vehicle(VehicleType.Military), tollPasses, 0};
            // Foreign vehicle should be charged
            yield return new object[] {new Vehicle(VehicleType.Foreign), tollPasses, 0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
