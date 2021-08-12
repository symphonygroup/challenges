export enum TollVehicles {
  CAR = 'Car',
  MOTORBIKE = 'Motorbike',
}

export enum TollFreeVehicles {
  MOTORCYCLE = 'Motorcycle',
  TRACTOR = 'Tractor',
  EMERGENCY = 'Emergency',
  DIPLOMAT = 'Diplomat',
  FOREIGN = 'Foreign',
  MILITARY = 'Military'
}

export type VehicleType = TollVehicles | TollFreeVehicles;
