namespace Garage;

public record Car(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 2;
    public Car(RegistrationNumber RegistrationNumber, VehicleColor Color)
        : this(RegistrationNumber, Color, _default_nr_wheels)
    {
        
    }
    
}