

using Garage;
using Garage.UI;

Garage.Garage garage = new Garage.Garage(5);
var ui = new UIHandler(garage);
ui.Run();


/*
garage class 
    wrapper for vehicle[]
    set capacity in the constructor

vehicle class
    registration number
    shared properties

subclasses:
    motorcycle
    airplane
    car
    bus
    boat

functionality
    list all parked vehicles
    list types of vehicles with counts
    add/remove vehicles
    possibility of specifying initial vehicles
    lookup by registration number
    search based on one or more properties, such as:
        all black vehicles with four wheels
        all pick motorcycles with 3 wheels
        all trucks
        all red vehicles
    success/fail feedback on all user actions
    if failed, specify why

    rebust to incorrect input

*/