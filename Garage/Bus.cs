namespace Garage;

public record Bus(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 8;
    public Bus(RegistrationNumber RegistrationNumber, VehicleColor Color)
        : this(RegistrationNumber, Color, _default_nr_wheels)
    {
        
    }
    
    public override string MakeNoise()
    {
        return "Hooonk Hooonk";
    }
}