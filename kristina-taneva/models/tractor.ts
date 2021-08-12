import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Tractor implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.TRACTOR;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}