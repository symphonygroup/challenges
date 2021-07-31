import { Vehicle } from ".";

export class Foreign implements Vehicle {
  getVehicleType(): string {
    return "Foreign";
  }
}
