import { TollFreeVehicles, TollVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Car implements Vehicle {
    getVehicleType(): VehicleType {
        return TollVehicles.CAR;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}
