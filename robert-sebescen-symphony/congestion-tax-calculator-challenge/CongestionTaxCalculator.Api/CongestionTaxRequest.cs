using System;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Api
{
    public class CongestionTaxRequest
    {
        public VehicleType VehicleType { get; set; }
        public DateTime[] TollPasses { get; set; }
        public string City { get; set; }
    }
}