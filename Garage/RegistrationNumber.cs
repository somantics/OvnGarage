namespace Garage;

public record RegistrationNumber(string Number)
{
    public static bool TryParse(string input, out RegistrationNumber result)
    {
        // TODO: add validation
        result = new RegistrationNumber(input);
        return true;
    }
}