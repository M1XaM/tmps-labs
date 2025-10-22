using System;

interface IVehicle
{
    void Move();
    IVehicle Clone();
}

class Car : IVehicle
{
    public string Name { get; set; }

    public Car() { }

    public Car(string name)
    {
        Name = name;
    }

    public void Move()
    {
        Console.WriteLine($"The car '{Name}' is driving on the road.");
    }
    
    public IVehicle Clone()
    {
        return new Car(this.Name);
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
        return new Bicycle();
    }
}

// Factory Pattern
class VehicleFactory
{
    public IVehicle CreateVehicle(string type, string name = "")
    {
        switch (type.ToLower())
        {
            case "car":
                return new Car(name);
            case "bicycle":
                return new Bicycle();
            default:
                throw new ArgumentException("Invalid vehicle type");
        }
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
        IVehicle factoryCar = factory.CreateVehicle("car", "Tesla");
        IVehicle factoryBike = factory.CreateVehicle("bicycle");
        factoryCar.Move();
        factoryBike.Move();

        Console.WriteLine("\nSingleton Pattern");
        VehicleLogger logger = VehicleLogger.Instance;
        logger.Log("Message from singleton object");

        Console.WriteLine("\nPrototype Pattern");
        Car originalCar = new Car("Ferrari");
        Car anotherCar = (Car)originalCar.Clone();
        
        originalCar.Move();
        anotherCar.Move();
    }
}