import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Emergency implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.EMERGENCY;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}