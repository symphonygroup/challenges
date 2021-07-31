import { Vehicle } from ".";

export class Motorcycle implements Vehicle {
  getVehicleType(): string {
    return "Motorcycle";
  }
}
