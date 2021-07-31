import { Vehicle } from ".";

export class Bus implements Vehicle {
  getVehicleType(): string {
    return "Bus";
  }
}
