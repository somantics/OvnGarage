
using System.ComponentModel.Design;
using System.Text;
using ConsoleMenu;
using ConsoleMenu.CLI;

namespace Garage.UI;

public class UIHandler
{
    private Garage _garage;
    private CLIClient _client;

    public UIHandler(Garage garage)
    {
        _garage = garage;
        _client = new CLIClient(CreateMainMenu());
    }
    
    public void Run()
    {
        _client.Run();
    }

    private Menu CreateMainMenu()
    {
        var menu = new OptionsMenu("Welcome to the main menu.", "Your option: ");

        var addVehicleSubmenu = new PromptMenu("Adding a vehicle.", "Enter registration number, type, amount of wheels: ", AddVehicle);
        menu.AddCommand("add", "Add a vehicle.", MenuOption.CreateOpenSubmenu(addVehicleSubmenu));

        var removeVehicleSubmenu = new PromptMenu("Removing a vehicle.", "Enter registration number: ", RemoveVehicle);
        menu.AddCommand("rm", "Remove a vehicle.", MenuOption.CreateOpenSubmenu(removeVehicleSubmenu));

        var getVehicleSubmenu = new PromptMenu("Requesting information on a vehicle.", "Enter registration number: ", GetVehicle);
        menu.AddCommand("get", "Remove a vehicle.", MenuOption.CreateOpenSubmenu(getVehicleSubmenu));

        menu.AddCommand("list", "List parked vehicles.", MenuOption.CreateOutputCommand(GetVehicleArray));
        menu.AddCommand("count", "List each parked vehicle type and respective counts.", MenuOption.CreateOutputCommand(GetVehicleTypes));
        menu.AddCommand("q", "Quit the applicaiton.", MenuOption.Close);

        return menu;
    }

    private bool RemoveVehicle(string input, out string result)
    {
        // is input a registration number?
        if (!RegistrationNumber.TryParse(input, out RegistrationNumber number))
        {
            result = "Could not recognize registration number. Is it in a valid format?";
            return false;
        }

        if (!_garage.TryRemove(number))
        {
            result = "No vehicle found with registration number {number}.";
            return false;
        }

        result = "Removed vehicle with registration number {number}.";
        return true;
    }

    private bool AddVehicle(string rawInput, out string result)
    {
        string[] input = rawInput.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // is the input actually a vehicle
        if (!Vehicle.TryParse(input, out Vehicle vehicle))
        {
            result = "Cannot interpret a vehicle from the input.";
            return false;
        }

        // is there room for the vehicle
        if (!_garage.TryAdd(vehicle))
        {
            result = "No room in the garage.";
            return false;
        }


        result = $"Added vehicle {vehicle.RegistrationNumber} to the garage.";
        return true;
    }

    private bool GetVehicle(string input, out string result)
    {
        // is input a registration number?
        if (!RegistrationNumber.TryParse(input, out RegistrationNumber number))
        {
            result = "Could not recognize registration number. Is it in a valid format?";
            return false;
        }

        Vehicle? vehicle = _garage.GetVehicle(number);
        if (vehicle is null)
        {
            result = "Could not find a vehicle matching that registration number.";
            return false;
        }

        result = $"Vehicle: {vehicle.RegistrationNumber} is a {vehicle.GetType()} with {vehicle.NumberOfWheels} wheels.";
        return true;
    }

    private bool GetVehicleArray(out string result)
    {
        var builder = new StringBuilder();
        Vehicle[] vehicles = _garage.GetVehicles();

        foreach (var vehicle in vehicles)
        {
            builder.AppendLine(vehicle.ToString());
        }

        result = builder.ToString();
        return true;
    }

    private bool GetVehicleTypes(out string result)
    {
        var builder = new StringBuilder();
        Dictionary<string, int> types = _garage.GetTypeCounts();

        foreach (var type in types)
        {
            builder.AppendLine(type.ToString());
        }

        result = builder.ToString();
        return true;
    }
}