namespace Garage;

public record NullVehicle(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 0;
    public NullVehicle()
        : this (new RegistrationNumber(""), VehicleColor.none, _default_nr_wheels)
    {
        
    }
}