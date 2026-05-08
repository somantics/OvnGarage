namespace Garage;

public record Airplane(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 5;
    public Airplane(RegistrationNumber RegistrationNumber, VehicleColor Color)
        : this(RegistrationNumber, Color, _default_nr_wheels)
    {
        
    }
}