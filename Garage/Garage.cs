
using System.Collections;

namespace Garage;

public class Garage(int capacity) : IEnumerable<Vehicle>
{
    private readonly int capacity = capacity;
    readonly List<Vehicle> _vehicles = [];

    public void Add(Vehicle vehicle)
    {
        TryAdd(vehicle);
    }

    public bool TryAdd(Vehicle vehicle)
    {
        if (capacity <= _vehicles.Count)
        {
            return false;
        }
        _vehicles.Add(vehicle);
        return true;
    }

    public bool TryRemove(Vehicle vehicle)
    {
        return TryRemove(vehicle.RegistrationNumber);
    }

    public bool TryRemove(RegistrationNumber registrationNumber)
    {
        Vehicle? match = _vehicles.Find(v => v.RegistrationNumber == registrationNumber);
        if (match is null)
        {
            return false;
        }

        _vehicles.Remove(match);
        return true;
    }

    public bool TryHonk(RegistrationNumber registrationNumber, out string result)
    {
        Vehicle? match = _vehicles.Find(v => v.RegistrationNumber == registrationNumber);
        if (match is null)
        {
            result = "Can't find a vehicle with matching registration number.";
            return false;
        }

        result = match.MakeNoise();
        return true;
    }

    public Vehicle? GetVehicle(RegistrationNumber registrationNumber)
    {
        return _vehicles.Find(v => v.RegistrationNumber == registrationNumber);
    }

    public IEnumerable<Vehicle> GetVehicles()
    {
        return _vehicles
            .Where(e => e is not null)
            .Select(e => e!);
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

    public IEnumerable<Vehicle> SearchVehicles(string[] input)
    {
        VehicleColor? searchColor = null;
        Type? searchVehicleType = null;
        int? searchWheelAmount = null;

        // Interpret search terms.
        foreach (string str in input)
        {
            if (searchColor is null && Vehicle.TryParseColor(str, out VehicleColor color))
            {
                searchColor = color;
                continue;
            }

            if (searchVehicleType is null && Vehicle.TryParseType(str, out Type type))
            {
                searchVehicleType = type;
                continue;
            }

            if (searchWheelAmount is null && int.TryParse(str, out int wheels))
            {
                searchWheelAmount = wheels;
                continue;
            }
        }

        // Perform search.
        var filtered = _vehicles.AsEnumerable();

        if (searchColor is not null)
        {
            filtered = filtered.Where(v => v.Color == searchColor);
        }

        if (searchVehicleType is not null)
        {
            filtered = filtered.Where(v => v.GetType() == searchVehicleType);
        }

        if (searchWheelAmount is not null)
        {
            filtered = filtered.Where(v => v.NumberOfWheels == searchWheelAmount);
        }

        return filtered;
    }

    public IEnumerator<Vehicle> GetEnumerator()
    {
        return _vehicles.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}