using System;
using congestion_tax_calculator_main.Enums;
using congestion_tax_calculator_main.Interfaces;

namespace congestion_tax_calculator_main.Models
{
    public class Vehicle : IVehicle
    {
        public VehicleTypes Type { get; set; }

        public String GetVehicleType()
        {
            return Type.ToString();
        }
    }
}