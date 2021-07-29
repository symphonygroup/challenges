using System.Collections.Generic;

namespace congestion.calculator.TollFee
{
    public interface ITollFreeVehicleProvider
    {
        bool IsTollFreeVehicle(Vehicle vehicle);
    }

    public class TollFreeVehicleProvider : ITollFreeVehicleProvider
    {
        private readonly HashSet<string> _tollFreeVehicles;

        /// <summary>
        ///     This could be set through configuration.
        /// </summary>
        public TollFreeVehicleProvider()
        {
            _tollFreeVehicles = new HashSet<string>
            {
                nameof(Military),
                nameof(Diplomat),
                nameof(Motorbike),
                nameof(Emergency),
                nameof(Foreign),
                nameof(Bus)
            };
        }

        public bool IsTollFreeVehicle(Vehicle vehicle) => _tollFreeVehicles.Contains(vehicle.GetVehicleType());
    }
}