import Car from "../models/car";
import Diplomat from "../models/diplomat";
import Emergency from "../models/emergency";
import Foreign from "../models/foreign";
import Military from "../models/military";
import Motorbike from "../models/motorbike";
import Motorcycle from "../models/motorcycle";
import Tractor from "../models/tractor";
import { TollFreeVehicles, TollVehicles, VehicleType } from "../models/types";
import Vehicle from "../models/vehicle";


export interface VehicleFactory {
    getVehicle(type: VehicleType): Vehicle;
}

export default class VehicleFactoryImpl implements VehicleFactory {
    private static vehicleTypeToClassMap = {
        [TollVehicles.CAR]: Car,
        [TollVehicles.MOTORBIKE]: Motorbike,
        [TollFreeVehicles.MOTORCYCLE]: Motorcycle,
        [TollFreeVehicles.DIPLOMAT]: Diplomat,
        [TollFreeVehicles.EMERGENCY]: Emergency,
        [TollFreeVehicles.MILITARY]: Military,
        [TollFreeVehicles.TRACTOR]: Tractor,
        [TollFreeVehicles.FOREIGN]: Foreign,
    }
    
    public getVehicle(type: TollVehicles.CAR): Car;
    public getVehicle(type: TollVehicles.MOTORBIKE): Motorbike;
    public getVehicle(type: TollFreeVehicles.MOTORCYCLE): Motorcycle;
    public getVehicle(type: TollFreeVehicles.DIPLOMAT): Diplomat;
    public getVehicle(type: TollFreeVehicles.TRACTOR): Tractor;
    public getVehicle(type: TollFreeVehicles.MILITARY): Military;
    public getVehicle(type: TollFreeVehicles.FOREIGN): Foreign;
    public getVehicle(type: TollFreeVehicles.EMERGENCY): Emergency;

    public getVehicle(type: VehicleType): Vehicle {
        const VehicleClass = VehicleFactoryImpl.vehicleTypeToClassMap[type];

        if (!VehicleClass) {
            throw new Error('Invalid vehicle type.');
        }

        return new VehicleClass();
    }
}

