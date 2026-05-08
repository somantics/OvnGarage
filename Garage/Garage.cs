
namespace Garage;

public class Garage(int capacity)
{
    readonly Vehicle?[] _vehicles = new Vehicle?[capacity];

    public void Add(Vehicle vehicle)
    {
        Console.WriteLine(vehicle.GetType());
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i] is null)
            {
                _vehicles[i] = vehicle;
                break;
            }
        }
    }

    public void Remove(Vehicle vehicle)
    {
        Remove(vehicle.RegistrationNumber);
    }

    public void Remove(RegistrationNumber registrationNumber)
    {
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i]?.RegistrationNumber == registrationNumber)
            {
                _vehicles[i] = null;
                break;
            }
        }
    }

    public Vehicle? GetVehicle(RegistrationNumber registrationNumber)
    {
        foreach (var vehicle in _vehicles)
        {
            if (vehicle?.RegistrationNumber == registrationNumber)
            {
                return vehicle;
            }
        }
        return null;
    }

    
}