import { Vehicle } from ".";

export class Military implements Vehicle {
  getVehicleType(): string {
    return "Military";
  }
}
