

using Garage;
using Garage.UI;

Garage<Vehicle> garage = new(8)
{
    new Airplane(new RegistrationNumber("abc"), VehicleColor.red),
    new Airplane(new RegistrationNumber("dge"), VehicleColor.black),
    new Airplane(new RegistrationNumber("jgh"), VehicleColor.blue),
    new Motorcycle(new RegistrationNumber("asd"), VehicleColor.red),
    new Motorcycle(new RegistrationNumber("esa"), VehicleColor.black),
    new Motorcycle(new RegistrationNumber("urs"), VehicleColor.blue)
};

var ui = new UIHandler(garage);
ui.Run();

