
namespace Garage;

public class Garage(int capacity)
{
    readonly Vehicle?[] _vehicles = new Vehicle?[capacity];

    public void Add(Vehicle vehicle)
    {
        TryAdd(vehicle);
    }

    public bool TryAdd(Vehicle vehicle)
    {
        Console.WriteLine(vehicle.GetType());
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i] is null)
            {
                _vehicles[i] = vehicle;
                return true;
            }
        }
        return false;
    }

    public bool TryRemove(Vehicle vehicle)
    {
        return TryRemove(vehicle.RegistrationNumber);
    }

    public bool TryRemove(RegistrationNumber registrationNumber)
    {
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i]?.RegistrationNumber == registrationNumber)
            {
                _vehicles[i] = null;
                return true;
            }
        }
        return false;
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

    public Vehicle[] GetVehicles()
    {
        return _vehicles
            .Where(e => e is not null)
            .Select(e => e!)
            .ToArray();
    }

    public Dictionary<string, int> GetTypeCounts()
    {
        Dictionary<string, int> vehicleCounts = [];

        foreach (var vehicle in GetVehicles())
        {
            string vehicleType = vehicle.GetType().ToString();

            if (!vehicleCounts.ContainsKey(vehicleType))
            {
                vehicleCounts.Add(vehicleType, 0);
            }

            vehicleCounts[vehicleType] ++;
        }

        return vehicleCounts;
    }

    
}