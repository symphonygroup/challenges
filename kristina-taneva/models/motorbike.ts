import { TollFreeVehicles, TollVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Motorbike implements Vehicle {
    getVehicleType(): VehicleType {
        return TollVehicles.MOTORBIKE;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}