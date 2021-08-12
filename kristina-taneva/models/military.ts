import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Military implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.MILITARY;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}