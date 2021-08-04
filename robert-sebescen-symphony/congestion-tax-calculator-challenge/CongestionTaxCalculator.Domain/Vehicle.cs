using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain
{
    public class Vehicle
    {
        public VehicleType Type { get; }

        public Vehicle(VehicleType type)
        {
            Type = type;
        }

        
    }
    public enum VehicleType
    {
        Car,
        Emergency,
        Bus,
        Diplomat,
        Motorcycle,
        Military,
        Foreign,
    }
}