import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Motorcycle implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.MOTORCYCLE;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}