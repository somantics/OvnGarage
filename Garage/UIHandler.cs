
using System.ComponentModel.Design;
using System.Text;
using ConsoleMenu.Menu;
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

        var addVehicleSubmenu = new PromptMenu("Adding a vehicle.", "Enter registration number, color, and type: ", AddVehicle);
        menu.AddOption("add", "Add a vehicle.", Menu.CreateOpenSubmenu(addVehicleSubmenu));

        var removeVehicleSubmenu = new PromptMenu("Removing a vehicle.", "Enter registration number: ", RemoveVehicle);
        menu.AddOption("rm", "Remove a vehicle.", Menu.CreateOpenSubmenu(removeVehicleSubmenu));

        var getVehicleSubmenu = new PromptMenu("Requesting information on a vehicle.", "Enter registration number: ", GetVehicle);
        menu.AddOption("get", "Remove a vehicle.", Menu.CreateOpenSubmenu(getVehicleSubmenu));

        var searchVehicleSubmenu = new PromptArrayMenu("Searching for vehicles matching attributes.", "Enter search terms separated by spaces: ", SearchVehicles);
        menu.AddOption("search", "Search for vehicle.", Menu.CreateOpenSubmenu(searchVehicleSubmenu));

        menu.AddOption("list", "List parked vehicles.", Menu.CreateOutputCommand(GetVehicleArray));
        menu.AddOption("count", "List each parked vehicle type and respective counts.", Menu.CreateOutputCommand(GetVehicleTypes));
        menu.AddOption("q", "Quit the applicaiton.", Menu.Close);

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

    private bool SearchVehicles(string[] input, out string result)
    {
        Vehicle?[] vehicles = _garage.SearchVehicles(input);

        var builder = new StringBuilder();
        foreach (var vehicle in vehicles)
        {
            if (vehicle is null)
            {
                continue;
            }
            builder.AppendLine(vehicle.ToString());
        }

        result = builder.ToString();
        return true;
    }
}