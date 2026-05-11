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

public abstract record Vehicle(RegistrationNumber RegistrationNumber, VehicleColor Color,int NumberOfWheels)
{
    public static bool TryParse(string[] inputs, out Vehicle result)
    {
        result = new NullVehicle();
        // check Length
        if (inputs.Length < 3)
        {
            return false;
        }

        // attempt to make registration number
        if (!RegistrationNumber.TryParse(inputs[0], out RegistrationNumber number))
        {
            return false;
        }

        // attempt to make Color
        if (!TryParseColor(inputs[1], out VehicleColor color))
        {
            return false;
        }

        // attempt to check type
        if (!TryParseType(inputs[2], out Type vehicleType))
        {
            return false;
        }

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
            _ => typeof(NullVehicle),
        };

        if (vehicleType == typeof(NullVehicle))
        {
            return false;
        }
        return true;
    }

    public virtual string MakeNoise()
    {
        return "If a vehicle makes a sound in a forest...";
    }
}