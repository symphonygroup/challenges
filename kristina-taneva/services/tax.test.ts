import VehicleFactoryImpl from "../factories/vehicle";
import { TollFreeVehicles, TollVehicles } from "../models/types";
import TaxServiceImpl from "./tax";

describe('TaxService', () => {
    describe('getTax', () => {
        it('should return 21 for Car vehicle and given dates', () => {
            const datesMock = ['2013-01-15 06:10:00', '2013-01-15 15:10:00'];
    
            const service = TaxServiceImpl.getInstance(new VehicleFactoryImpl());
    
            const result = service.getTax(TollVehicles.CAR, datesMock);
            expect(result).toBe(21);
        })
    
        it('should return 13 for Car vehicle and given dates', () => {
            const datesMock = ['2013-01-15 06:10:00', '2013-01-15 06:35:00'];
    
            const service = TaxServiceImpl.getInstance(new VehicleFactoryImpl());
    
            const result = service.getTax(TollVehicles.CAR, datesMock);
            expect(result).toBe(13);
        })
    
        it('should return 60 for Car vehicle when toll total cost is more than 60', () => {
            const datesMock = ['2013-01-15 07:15:00', '2013-01-15 08:28:00', '2013-01-15 09:45:00','2013-01-15 11:00:00', '2013-01-15 13:00:00', '2013-01-15 14:20:00', '2013-01-15 16:50:00'];
    
            const service = TaxServiceImpl.getInstance(new VehicleFactoryImpl());
    
            const result = service.getTax(TollVehicles.CAR, datesMock);
            expect(result).toBe(60);
        })
    
        it('should return 0 for toll free date', () => {
            const datesMock = ['2013-01-01 07:15:00', '2013-01-01 08:28:00'];
    
            const service = TaxServiceImpl.getInstance(new VehicleFactoryImpl());
    
            const result = service.getTax(TollVehicles.CAR, datesMock);
            expect(result).toBe(0);
        })

        it('should return 0 for toll free vehicle', () => {
            const datesMock = ['2013-01-15 07:15:00', '2013-01-15 08:28:00', '2013-01-15 09:45:00','2013-01-15 11:00:00', '2013-01-15 13:00:00', '2013-01-15 14:20:00', '2013-01-15 16:50:00'];
    
            const service = TaxServiceImpl.getInstance(new VehicleFactoryImpl());
    
            const result = service.getTax(TollFreeVehicles.DIPLOMAT, datesMock);
            expect(result).toBe(0); 
        })
    })
})