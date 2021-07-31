import { Vehicle } from ".";

export class Car implements Vehicle {
  getVehicleType(): string {
    return "Car";
  }
}
