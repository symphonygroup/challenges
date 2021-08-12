import { VehicleType } from "./types";

interface Vehicle {
    getVehicleType(): VehicleType;
    isTollFreeVehicle(): boolean;
}
export default Vehicle;