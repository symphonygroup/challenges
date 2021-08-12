import Vehicle from "../models/vehicle";

export const getTollFee = (date: Date, vehicle: Vehicle): number => {
    if(isTollFreeDate(date) || vehicle.isTollFreeVehicle()) {
        return 0;
    }
    
    const hour:number = date.getHours();
    const minute:number = date.getMinutes();

    if (hour == 6 && minute >= 0 && minute <= 29) {
        return 8;
    } else if (hour == 6 && minute >= 30 && minute <= 59) {
        return 13;
    } else if (hour == 7) {
        return 18;
    } else if (hour == 8 && minute >= 0 && minute <= 29) {
        return 13;
    } else if (hour == 8 && minute >= 30 || (hour >= 9 && hour <= 14)) {
        return 8;
    } else if (hour == 15 && minute >= 0 && minute <= 29) {
        return 13;
    } else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) {
        return 18;
    } else if (hour == 17) {
        return 13;
    } else if (hour == 18 && minute >= 0 && minute <= 29) {
        return 8;
    }
    
    return 0;
}

export const isTollFreeDate = (date: Date): boolean => {
    const year:number = date.getFullYear();
    const month:number = date.getMonth() + 1;
    const day:number = date.getDay() + 1;
    const dayOfMonth:number = date.getDate();

    const isWeekend = day === 7 || day === 1;
    const isMonthJuly = month === 7;
    const publicHolidays: Record<number, Array<number>> = {
        1: [1],
        3: [28, 29],
        4: [1, 30],
        5: [1, 8, 9],
        6: [5, 6, 21],
        11: [1],
        12: [24,25,26,31],
    }

    const publicHolidayMonth = publicHolidays[month];

    if (isWeekend || isMonthJuly) {
        return true;
    }

    if (year === 2013 && publicHolidayMonth) {
         return publicHolidayMonth.includes(dayOfMonth);
    }

    return false;
}