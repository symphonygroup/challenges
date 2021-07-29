namespace congestion.calculator
{
    public interface Vehicle
    {
        string GetVehicleType();
    }

    public class Car : Vehicle
    {
        public string GetVehicleType() => nameof(Car);
    }

    public class Motorbike : Vehicle
    {
        public string GetVehicleType() => nameof(Motorbike);
    }

    public class Military : Vehicle
    {
        public string GetVehicleType() => nameof(Military);
    }

    public class Diplomat : Vehicle
    {
        public string GetVehicleType() => nameof(Diplomat);
    }

    public class Emergency : Vehicle
    {
        public string GetVehicleType() => nameof(Emergency);
    }

    public class Bus : Vehicle
    {
        public string GetVehicleType() => nameof(Bus);
    }
    
    public class Foreign : Vehicle
    {
        public string GetVehicleType() => nameof(Foreign);
    }
}