
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

    public bool TryHonk(RegistrationNumber registrationNumber, out string result)
    {
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i] is null)
            {
                continue;
            }
            else if (_vehicles[i].RegistrationNumber == registrationNumber)
            {
                result = _vehicles[i].MakeNoise();
                return true;
            }
        }
        result = "Can't find a vehicle with matching registration number.";
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

    
}