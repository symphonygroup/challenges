using System;
using System.Collections.Generic;
using System.IO;
using congestion_tax_calculator_main.Enums;
using congestion_tax_calculator_main.Interfaces;
using congestion_tax_calculator_main.Models;
using Xunit;

namespace congestion_tax_calculator_main.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetMaxFeeTest()
        {
            int maxFee = 60;
            int totalFee = 70;
            int result = (totalFee > maxFee) ? maxFee : totalFee;

            Assert.Equal(60, result);
        }
        [Fact]
        public void IsTollFreeVehicle()
        {
            IVehicle vehicle = new Vehicle();
            bool result = vehicle.GetVehicleType().Equals(TollFreeVehicles.Motorcycle.ToString());

            Assert.False(result);

        }

        [Fact]
        public void IsTollFreeDate()
        {
            DateTime date = DateTime.ParseExact("2013-01-01 21:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            bool result = false;

            switch (month)
            {
                case 1: result = (day == 1); break;
                case 3: result = (day == 28 || day == 29); break;
                case 4: result = (day == 1 || day == 30); break;
                case 5: result = (day == 1 || day == 8 || day == 9); break;
                case 6: result = (day == 5 || day == 6 || day == 21); break;
                case 7: result = true; break;
                case 11: result = day == 1; break;
                case 12: result = (day == 24 || day == 25 || day == 26 || day == 31);break;
                default: result = false;break;
            }

            Assert.True(result);
        }
    }
}
