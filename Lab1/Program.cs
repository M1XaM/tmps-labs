using System;

interface IVehicle
{
    void Move();
    IVehicle Clone();
}

class Car : IVehicle
{
    public void Move()
    {
        Console.WriteLine("The car is driving on the road.");
    }
    
    public IVehicle Clone()
    {
        return new Car(); // simple clone
    }
}

class Bicycle : IVehicle
{
    public void Move()
    {
        Console.WriteLine("The bicycle is pedaling along the path.");
    }
    
    public IVehicle Clone()
    {
        return new Bicycle(); // simple clone
    }
}

// 1. Factory Pattern
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

// 3. Prototype Pattern
class VehiclePrototype
{
    private IVehicle _carPrototype = new Car();
    private IVehicle _bikePrototype = new Bicycle();
    
    public IVehicle CreateCar()
    {
        return _carPrototype.Clone();
    }
    
    public IVehicle CreateBicycle()
    {
        return _bikePrototype.Clone();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Factory Pattern");
        VehicleFactory factory = new VehicleFactory();
        IVehicle factoryCar = factory.CreateVehicle("car");
        IVehicle factoryBike = factory.CreateVehicle("bicycle");
        factoryCar.Move();
        factoryBike.Move();

        Console.WriteLine("\nSingleton Pattern");
        VehicleLogger logger = VehicleLogger.Instance;
        logger.Log("Message from singleton object");

        Console.WriteLine("\nPrototype Pattern");
        IVehicle originalCar = new Car();
        IVehicle anotherCar = originalCar.Clone();
        
        originalCar.Move();
        anotherCar.Move();
    }
}