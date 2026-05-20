
using System.Collections;

namespace Garage;

public class Garage<T>(int capacity) : IEnumerable<T>
    where T: IRegisterable, IHonkable, ISearchable
{
    readonly T?[] _vehicles = new T?[capacity];

    public void Add(T item)
    {
        TryAdd(item);
    }


    public bool TryAdd(T item)
    {
        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i] is null)
            {
                _vehicles[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool TryRemove(T item)
    {
        return TryRemove(item.GetRegistrationNumber());
    }

    public bool TryRemove(RegistrationNumber registrationNumber)
    {

        for (int i = 0; i < _vehicles.Length ; i++)
        {
            if (_vehicles[i]?.GetRegistrationNumber() == registrationNumber)
            {
                _vehicles[i] = default;
                return true;
            }
        }
        return false;
    }

    public bool TryHonk(RegistrationNumber registrationNumber, out string result)
    {
        T? match = GetVehicles().Find(v => v.GetRegistrationNumber() == registrationNumber);
        if (match is null)
        {
            result = "Can't find a vehicle with matching registration number.";
            return false;
        }

        result = match.MakeNoise();
        return true;
    }

    public T? GetVehicle(RegistrationNumber registrationNumber)
    {
        return GetVehicles().Find(v => v.GetRegistrationNumber() == registrationNumber);
    }

    public List<T> GetVehicles()
    {
        return _vehicles
            .Where(e => e is not null)
            .Select(e => e!)
            .ToList();
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

    public IEnumerable<T> SearchVehicles(string[] input)
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
        IEnumerable<T> filtered = GetVehicles();

        if (searchColor is not null)
        {
            filtered = filtered.Where(v => v.GetColor() == searchColor);
        }

        if (searchVehicleType is not null)
        {
            filtered = filtered.Where(v => v.GetType() == searchVehicleType);
        }

        if (searchWheelAmount is not null)
        {
            filtered = filtered.Where(v => v.GetWheelCount() == searchWheelAmount);
        }

        return filtered;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _vehicles)
        {
            if (item is null) continue;
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}