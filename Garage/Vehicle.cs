namespace Garage;
public enum VehicleColor
{
    red,
    blue,
    black,
}

public abstract record Vehicle(RegistrationNumber RegistrationNumber, VehicleColor Color,int NumberOfWheels)
{
    public static bool TryParse(string[] inputs, out Vehicle result)
    {
        throw new NotImplementedException();
    }
}