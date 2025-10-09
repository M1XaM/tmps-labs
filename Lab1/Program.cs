using System;

interface IVehicle
{
    void Move();
}

class Car : IVehicle
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

// 1. Simple Factory Pattern
class VehicleFactory
{
    public IVehicle CreateVehicle(string type)
    {
        switch (type.ToLower())
        {
            case "car":
                return new Car();
            case "bicycle":
                return new Bicycle();
            default:
                throw new ArgumentException("Invalid vehicle type");
        }
    }
}

// 2. Singleton Pattern
class VehicleLogger
{
    private static VehicleLogger _instance;
    
    private VehicleLogger() { }
    
    public static VehicleLogger Instance
    {
        get
        {
            _instance ??= new VehicleLogger();
            return _instance;
        }
    }
    
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

// 3. Builder Pattern
class VehicleBuilder
{
    private string _type = "car";
    
    public VehicleBuilder SetType(string type)
    {
        _type = type;
        return this;
    }
    
    public IVehicle Build()
    {
        var factory = new VehicleFactory();
        return factory.CreateVehicle(_type);
    }
}

class VehicleController
{
    private readonly IVehicle _vehicle;

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
        new VehicleController(car).StartMoving();

        IVehicle bike = new Bicycle();
        new VehicleController(bike).StartMoving();

        Console.WriteLine("\n=== Factory Pattern ===");
        VehicleFactory factory = new VehicleFactory();
        IVehicle factoryCar = factory.CreateVehicle("car");
        IVehicle factoryBike = factory.CreateVehicle("bicycle");
        factoryCar.Move();
        factoryBike.Move();

        Console.WriteLine("\n=== Singleton Pattern ===");
        VehicleLogger logger = VehicleLogger.Instance;
        logger.Log("Application started");
        VehicleLogger.Instance.Log("Vehicle created");

        Console.WriteLine("\n=== Builder Pattern ===");
        IVehicle builtCar = new VehicleBuilder().SetType("car").Build();
        IVehicle builtBike = new VehicleBuilder().SetType("bicycle").Build();
        builtCar.Move();
        builtBike.Move();
    }
}