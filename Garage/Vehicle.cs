namespace Garage;
public enum VehicleColor
{
    red,
    blue,
    black,
    yellow,
    grey,
    pink,
    green,
    magenta,
    purple,
    orange,
    white,
    brown,

    none,
}

public abstract record Vehicle(RegistrationNumber RegistrationNumber, VehicleColor Color,int NumberOfWheels) : IRegisterable, IHonkable, ISearchable
{
    public static bool TryParse(string[] inputs, out Vehicle result, out string issue)
    {
        result = new NullVehicle();
        // check Length
        if (inputs.Length < 3)
        {
            issue = "Too few arugments.";
            return false;
        }

        // attempt to make registration number
        if (!RegistrationNumber.TryParse(inputs[0], out RegistrationNumber number))
        {
            issue = $"Could not parse registration number from {inputs[0]}";
            return false;
        }

        // attempt to make Color
        if (!TryParseColor(inputs[1], out VehicleColor color))
        {
            issue = $"Could not parse color from {inputs[1]}";
            return false;
        }

        // attempt to check type
        if (!TryParseType(inputs[2], out Type vehicleType))
        {
            issue = $"Could not parse vehicle type from {inputs[2]}";
            return false;
        }
        issue = "";
        result = (Vehicle)Activator.CreateInstance(vehicleType, [number, color]); // risky business here

        return true;
    }

    public static bool TryParseColor(string input, out VehicleColor color)
    {
        color = input.ToLower() switch
        {
            "red" => VehicleColor.red,
            "blue" => VehicleColor.blue,
            "black" => VehicleColor.black,
            "yellow" => VehicleColor.yellow,
            "grey" => VehicleColor.grey,
            "green" => VehicleColor.green,
            "pink" => VehicleColor.pink,
            "magenta" => VehicleColor.magenta,
            "purple" => VehicleColor.purple,
            "orange" => VehicleColor.orange,
            "white" => VehicleColor.white,
            "brown" => VehicleColor.brown,
            _ => VehicleColor.none,
        };

        if (color == VehicleColor.none)
        {
            return false;
        }
        return true;
    }

    public static bool TryParseType(string input, out Type vehicleType)
    {
        vehicleType = input.ToLower() switch
        {
            "motorcycle" => typeof(Motorcycle),
            "airplane" => typeof(Airplane),
            "bus" => typeof(Bus),
            "car" => typeof(Car),
            "boat" => typeof(Boat),
            _ => typeof(NullVehicle),
        };

        if (vehicleType == typeof(NullVehicle))
        {
            return false;
        }
        return true;
    }

    public RegistrationNumber GetRegistrationNumber()
    {
        return RegistrationNumber;
    }

    public VehicleColor GetColor()
    {
        return Color;
    }

    public int GetWheelCount()
    {
        return NumberOfWheels;
    }

    public virtual string MakeNoise()
    {
        return "If a vehicle makes a sound in a forest...";
    }
}