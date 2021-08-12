import Car from "../models/car";
import Foreign from "../models/foreign";
import Motorbike from "../models/motorbike";
import { isTollFreeDate, getTollFee } from "./tax";

describe('isTollFreeDate', () => {
    it('should return true for month July', () => {
        const dateMock = new Date('2013-07-14 19:10:00');

        const result = isTollFreeDate(dateMock);
        expect(result).toBeTruthy();
    })

    it('should return true for a public holiday', () => {
        const dateMock = new Date('2013-01-01 19:10:00');

        const result = isTollFreeDate(dateMock);
        expect(result).toBeTruthy();
    })

    it('should return true for Saturday', () => {
        const dateMock = new Date('2013-01-13 19:10:00');

        const result = isTollFreeDate(dateMock);
        expect(result).toBeTruthy();
    })

    it('should return false for Monday', () => {
        const dateMock = new Date('2013-01-15 19:10:00');

        const result = isTollFreeDate(dateMock);
        expect(result).toBeFalsy();
    })
})

describe('getTollFee', () => {
    it('should return 0 for a foreign vehicle', () => {
        const foreignMock = new Foreign();
        const dateMock = new Date('2013-01-14 18:00:00');

        const result = getTollFee(dateMock, foreignMock);
        expect(result).toBe(0);
    })

    it('should return 8 for a car vehicle', () => {
        const carMock = new Car();
        const dateMock = new Date('2013-01-14 18:00:00');

        const result = getTollFee(dateMock, carMock);
        expect(result).toBe(8);
    })

    it('should return 13 for a motorbike', () => {
        const motorbikeMock = new Motorbike();
        const dateMock = new Date('2013-01-14 15:10:00');

        const result = getTollFee(dateMock, motorbikeMock);
        expect(result).toBe(13);
    })

    it('should return 0 for a car vehicle after 18:30', () => {
        const carMock = new Car();
        const dateMock = new Date('2013-01-14 19:10:00');

        const result = getTollFee(dateMock, carMock);
        expect(result).toBe(0);
    })
})