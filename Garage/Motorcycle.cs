namespace Garage;

public record Motorcycle(RegistrationNumber RegistrationNumber, int NumberOfWheels) : Vehicle(RegistrationNumber, NumberOfWheels);