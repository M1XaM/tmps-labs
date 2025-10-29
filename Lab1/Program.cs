using System;

// Prototype Pattern
interface IVehiclePrototype
{
    IVehicle Clone();
}

interface IVehicle
{
    void Move();
}

class Car : IVehicle, IVehiclePrototype
{
    public string Name { get; set; }

    public Car() { }

    public Car(string name)
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine($"The car {Name} is driving on the road.");
    }
    
    public IVehicle Clone()
    {
        return new Car(this.Name);
    }
}

class Bicycle : IVehicle, IVehiclePrototype
{
    public string Name { get; set; }

    public Bicycle() { }

    public Bicycle(string name)
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine($"The bicycle {Name} is pedaling along the path.");
    }
    
    public IVehicle Clone()
    {
        return new Bicycle(this.Name);
    }
}

// Factory Pattern
class VehicleFactory
{
    private readonly Dictionary<string, Func<string, IVehicle>> _registry = new();

    public void RegisterVehicle(string type, Func<string, IVehicle> creator)
    {
        _registry[type.ToLower()] = creator;
    }

    public IVehicle CreateVehicle(string type, string name = "")
    {
        if (_registry.TryGetValue(type.ToLower(), out var creator))
            return creator(name);
        throw new ArgumentException($"Invalid vehicle type: {type}");
    }
}

// Singleton Pattern
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

class Program
{
    static void Main()
    {
        Console.WriteLine("Factory Pattern");
        VehicleFactory factory = new VehicleFactory();
        factory.RegisterVehicle("car", name => new Car(name));
        factory.RegisterVehicle("bicycle", name => new Bicycle(name));

        IVehicle factoryCar = factory.CreateVehicle("car", "Tesla");
        IVehicle factoryBike = factory.CreateVehicle("bicycle", "SuperBike");
        factoryCar.Move();
        factoryBike.Move();

        Console.WriteLine("\nSingleton Pattern");
        VehicleLogger logger = VehicleLogger.Instance;
        logger.Log("Message from singleton object");

        Console.WriteLine("\nPrototype Pattern");
        Car originalCar = new Car("Ferrari");
        IVehicle anotherCar = originalCar.Clone();
        
        originalCar.Move();
        anotherCar.Move();
    }
}
