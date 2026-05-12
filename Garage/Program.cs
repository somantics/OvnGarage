

using Garage;
using Garage.UI;

Garage.Garage garage = new Garage.Garage(8);
garage.Add(new Airplane(new RegistrationNumber("abc"), VehicleColor.red));
garage.Add(new Airplane(new RegistrationNumber("dge"), VehicleColor.black));
garage.Add(new Airplane(new RegistrationNumber("jgh"), VehicleColor.blue));
garage.Add(new Motorcycle(new RegistrationNumber("asd"), VehicleColor.red));
garage.Add(new Motorcycle(new RegistrationNumber("esa"), VehicleColor.black));
garage.Add(new Motorcycle(new RegistrationNumber("urs"), VehicleColor.blue));

var ui = new UIHandler(garage);
ui.Run();

