using System;

interface IVehicle
{
    void Move();
}

class Car : IVehicle // OCP: Open for extension, closed for modification
{
    public void Move()
    {
        Console.WriteLine("The car is driving on the road.");
    }
}

class Bicycle : IVehicle
{
    public void Move()
    {
        Console.WriteLine("The bicycle is pedaling along the path.");
    }
}

class VehicleController // SRP: VehicleController only controls vehicles, not their movement logic
{
    private readonly IVehicle _vehicle; // DIP: Depends on abstraction, not concrete vehicle

    public VehicleController(IVehicle vehicle)
    {
        _vehicle = vehicle;
    }

    public void StartMoving()
    {
        _vehicle.Move();
    }
}

class Program
{
    static void Main()
    {
        IVehicle car = new Car();
        VehicleController carController = new VehicleController(car);
        carController.StartMoving();

        IVehicle bike = new Bicycle();
        VehicleController bikeController = new VehicleController(bike);
        bikeController.StartMoving();
    }
}
