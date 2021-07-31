import { Vehicle } from ".";

export class Emergency implements Vehicle {
  getVehicleType(): string {
    return "Emergency";
  }
}
