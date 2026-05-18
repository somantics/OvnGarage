
namespace Garage.Test;

public class GarageTest
{

    private const string RegNr1 = "Abc123";
    private const string RegNr2 = "Abc124";
    private const string RegNr3 = "Abc125";
    private const string NewRegNr = "Abc555";

    [Fact]
    public void AddSuccedsWhenThereIsSpace()
    {
        var garage = new Garage(3);
        var vehicles = MakeThreeVehicles();

        foreach (var item in vehicles)
        {
            garage.Add(item);
        }
        var parkedVehicles = garage.GetVehicles();

        Assert.Equal(3, parkedVehicles.Count());
    }

    [Fact]
    public void AddFailsQuietlyWithoutSpace()
    {
        var garage = new Garage(2);
        var vehicles = MakeThreeVehicles();

        foreach (var item in vehicles)
        {
            garage.Add(item);
        }
        var parkedVehicles = garage.GetVehicles();

        Assert.Equal(2, parkedVehicles.Count());
    }

    [Fact]
    public void TryAddSuccedsWhenThereIsSpace()
    {
        var garage = new Garage(3);
        var vehicles = MakeThreeVehicles();

        garage.TryAdd(vehicles[0]);
        garage.TryAdd(vehicles[1]);
        bool thirdAttempt = garage.TryAdd(vehicles[2]);

        var parkedVehicles = garage.GetVehicles();

        Assert.True(thirdAttempt);
        Assert.Equal(3, parkedVehicles.Count());
    }

    [Fact]
    public void TryAddFailsWhenOutOfSpace()
    {
        //arrange
        var garage = new Garage(2);
        var vehicles = MakeThreeVehicles();

        //act
        garage.TryAdd(vehicles[0]);
        garage.TryAdd(vehicles[1]);
        bool thirdAttempt = garage.TryAdd(vehicles[2]);

        var parkedVehicles = garage.GetVehicles();

        //assert
        Assert.False(thirdAttempt);
        Assert.Equal(2, parkedVehicles.Count());
    }

    [Fact]
    public void TryRemoveCorrectlyRemovesAMatch()
    {
        Garage garage = MakeGarageWithThreeVehicles();

        garage.TryRemove(new RegistrationNumber(RegNr2));
        var vehicles = garage.GetVehicles();
        var registrationNumbers = vehicles
            .Select(v => v.RegistrationNumber)
            .ToList();

        Assert.DoesNotContain(new RegistrationNumber(RegNr2), registrationNumbers);
        Assert.Equal(2, vehicles.Count());
        Assert.Contains(new RegistrationNumber(RegNr1), registrationNumbers);
        Assert.Contains(new RegistrationNumber(RegNr3), registrationNumbers);
    }

    [Fact]
    public void TryRemoveFailsWithoutMatch()
    {
        Garage garage = MakeGarageWithThreeVehicles();
        
        bool success = garage.TryRemove(new RegistrationNumber(NewRegNr));

        var registrationNumbers = garage.GetVehicles()
            .Select(v => v.RegistrationNumber)
            .ToList();

        Assert.False(success);
        Assert.Equal(3, registrationNumbers.Count);
        Assert.Contains(new RegistrationNumber(RegNr1), registrationNumbers);
        Assert.Contains(new RegistrationNumber(RegNr2), registrationNumbers);
        Assert.Contains(new RegistrationNumber(RegNr3), registrationNumbers);
    }

    private static Garage MakeGarageWithThreeVehicles()
    {
        var garage = new Garage(3);
        foreach (var item in MakeThreeVehicles())
        {
            garage.Add(item);
        }

        return garage;
    }

    private static Vehicle[] MakeThreeVehicles()
    {
        var regNumber = new RegistrationNumber(RegNr1);
        var regNumber2 = new RegistrationNumber(RegNr2);
        var regNumber3 = new RegistrationNumber(RegNr3);
        Vehicle vehicle = new Car(regNumber, VehicleColor.red, 4);
        Vehicle vehicle2 = new Car(regNumber2, VehicleColor.red, 4);
        Vehicle vehicle3 = new Car(regNumber3, VehicleColor.red, 4);
        return [vehicle, vehicle2, vehicle3];
    }
}
