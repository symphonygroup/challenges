import { TollFreeVehicles, VehicleType } from "./types";
import Vehicle from "./vehicle";

export default class Foreign implements Vehicle {
    getVehicleType(): VehicleType {
        return TollFreeVehicles.FOREIGN;
    }
    isTollFreeVehicle(): boolean { 
        const vehicleType = this.getVehicleType();
        return Object.values(TollFreeVehicles).includes(vehicleType as TollFreeVehicles);
    }
}