namespace Garage;

public record Airplane(RegistrationNumber RegistrationNumber, int NumberOfWheels) : Vehicle(RegistrationNumber, NumberOfWheels);