import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Diplomat implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.DIPLOMAT;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}