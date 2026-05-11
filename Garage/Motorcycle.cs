namespace Garage;

public record Motorcycle(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels)
{
    private const int _default_nr_wheels = 2;
    public Motorcycle(RegistrationNumber RegistrationNumber, VehicleColor Color)
        : this(RegistrationNumber, Color, _default_nr_wheels)
    {
    }

    public override string MakeNoise()
    {
        return "Zoomzoom";
    }
}