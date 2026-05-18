
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

    public Vehicle?[] SearchVehicles(string[] input)
    {
        VehicleColor? searchColor = null;
        Type? searchVehicleType = null;
        int? searchWheelAmount = null;
        Vehicle?[] matchingVehicles = new Vehicle[capacity];

        // interpret search terms
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

        // perform search
        int foundCounter = 0;
        foreach (var vehicle in _vehicles)
        {
            if (vehicle is null)
            {
                // spot is empty
                continue;
            }

            if (searchVehicleType != null && vehicle.GetType() != searchVehicleType)
            {
                // wrong type, not wanted
                continue;
            }

            if (searchColor != null && vehicle.Color != searchColor)
            {
                // wrong color, not wanted
                continue;
            }

            if (searchWheelAmount != null && vehicle.NumberOfWheels != searchWheelAmount)
            {
                // wrong number of wheels, not wanted
                continue;
            }

            // Anything here is wanted
            matchingVehicles[foundCounter] = vehicle;
            foundCounter++;
        }

        return matchingVehicles;
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