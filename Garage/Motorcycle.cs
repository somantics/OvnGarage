namespace Garage;

public record Motorcycle(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels);