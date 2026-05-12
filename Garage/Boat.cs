namespace Garage;

public record Boat(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 0;
    public Boat(RegistrationNumber RegistrationNumber, VehicleColor Color)
        : this(RegistrationNumber, Color, _default_nr_wheels)
    {
        
    }
    public override string MakeNoise()
    {
        return "Glug glug";
    }
}