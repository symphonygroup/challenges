import { VehicleType } from "../models/types";
import { VehicleFactory } from "../factories/vehicle";
import { getTollFee } from "../utils/tax";

interface TaxService {
    getTax(vehicleType: VehicleType, dateStrings: string[]): number;
}

export default class TaxServiceImpl implements TaxService {
    private constructor(private vehicleFactory: VehicleFactory) {
    }

    private static instance: TaxServiceImpl;

    public static getInstance(vehicleFactory: VehicleFactory): TaxServiceImpl {
        if (!this.instance) {
            this.instance = new TaxServiceImpl(vehicleFactory);
        }

        return this.instance;
    }

    getTax(vehicleType: VehicleType, dateStrings: string[]): number {
        const vehicle = this.vehicleFactory.getVehicle(vehicleType);
        const dates = dateStrings.map(date => new Date(date));

        let totalFee = dates.reduce((acc, current) => {
            const intervalStart = dates[0];
            const nextFee = getTollFee(current, vehicle);
            let tempFee = getTollFee(intervalStart, vehicle);

            const diffInMillis = current.getTime() - intervalStart.getTime();

            const minutes = diffInMillis / 1000 / 60;

            if (minutes <= 60) {
                if (acc > 0) {
                    acc -= tempFee;
                }

                if (nextFee >= tempFee) {
                    tempFee = nextFee;
                }

                return acc + tempFee;
            }

            return acc + nextFee;
        }, 0);

        if (totalFee > 60) {
            totalFee = 60;
        }

        return totalFee;
    }
}

