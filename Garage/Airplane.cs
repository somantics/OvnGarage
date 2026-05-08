namespace Garage;

public record Airplane(RegistrationNumber RegistrationNumber, VehicleColor Color, int NumberOfWheels) : Vehicle(RegistrationNumber, Color, NumberOfWheels);