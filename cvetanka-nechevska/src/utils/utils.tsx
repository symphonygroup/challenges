import { Vehicle } from "../models";

import { TollFreeVehicles } from "../enums/TollFreeVehicles";

export function isTollFreeDate(date: Date): boolean {
  const year: number = date.getFullYear();
  const month: number = date.getMonth() + 1;
  const day: number = date.getDay();
  const dayOfMonth: number = date.getDate();

  if (day === 6 || day === 0) return true;

  if (year === 2013) {
    if (
      (month === 1 && dayOfMonth === 1) ||
      (month === 3 &&
        (dayOfMonth === 28 || dayOfMonth === 29 || dayOfMonth === 31)) ||
      (month === 4 && (dayOfMonth === 1 || dayOfMonth === 30)) ||
      (month === 5 &&
        (dayOfMonth === 1 || dayOfMonth === 8 || dayOfMonth === 9)) ||
      (month === 6 &&
        (dayOfMonth === 5 || dayOfMonth === 6 || dayOfMonth === 21)) ||
      month === 7 ||
      (month === 11 && dayOfMonth === 1) ||
      (month === 12 &&
        (dayOfMonth === 23 ||
          dayOfMonth === 24 ||
          dayOfMonth === 25 ||
          dayOfMonth === 26 ||
          dayOfMonth === 30 ||
          dayOfMonth === 31))
    ) {
      return true;
    }
  }
  return false;
}

export function isTollFreeVehicle(vehicle: Vehicle): boolean {
  const vehicleType: string = vehicle.getVehicleType();

  return (
    vehicleType == TollFreeVehicles[TollFreeVehicles.Bus] ||
    vehicleType == TollFreeVehicles[TollFreeVehicles.Diplomat] ||
    vehicleType == TollFreeVehicles[TollFreeVehicles.Emergency] ||
    vehicleType == TollFreeVehicles[TollFreeVehicles.Foreign] ||
    vehicleType == TollFreeVehicles[TollFreeVehicles.Military] ||
    vehicleType == TollFreeVehicles[TollFreeVehicles.Motorcycle]
  );
}

export function getTollFee(date: Date): number {
  const dateAndTime = new Date(date);
  if (isTollFreeDate(dateAndTime)) return 0;

  const hour: number = dateAndTime.getHours();
  const minutes: number = dateAndTime.getMinutes();

  if (hour === 6 && minutes >= 0 && minutes <= 29) return 8;
  else if (hour === 6 && minutes >= 30 && minutes <= 59) return 13;
  else if (hour === 7 && minutes >= 0 && minutes <= 59) return 18;
  else if (hour === 8 && minutes >= 0 && minutes <= 29) return 13;
  else if (hour >= 8 && hour <= 14 && minutes >= 30 && minutes <= 59) return 8;
  else if (hour === 15 && minutes >= 0 && minutes <= 29) return 13;
  else if (hour === 15 && hour <= 16 && minutes >= 30 && minutes <= 59)
    return 18;
  else if (hour === 17 && minutes >= 0 && minutes <= 59) return 13;
  else if (hour === 18 && minutes >= 0 && minutes <= 29) return 8;
  else return 0;
}

export function getTax(vehicle: Vehicle, dates: Date[]) {
  if (isTollFreeVehicle(vehicle)) return 0;

  let intervalStart: Date = new Date(dates[0]);
  let totalFee: number = 0;

  for (let i = 0; i < dates.length; i++) {
    const date: Date = new Date(dates[i]);

    let nextFee: number = getTollFee(date);
    let tempFee = getTollFee(intervalStart);

    let diffInMillies = date.getTime() - intervalStart.getTime();
    let minutes = diffInMillies / 1000 / 60;

    if (minutes <= 60) {
      if (totalFee > 0) totalFee -= tempFee;
      if (nextFee >= tempFee) tempFee = nextFee;
      totalFee += tempFee;
    } else {
      totalFee += nextFee;
    }
    if (totalFee > 60) totalFee = 60;
  }

  return totalFee;
}
