using System;
using System.Collections.Generic;

public interface ICombustionEngine
{
    void Refuel();
}

public class GasolineCar : ICombustionEngine
{
    public void Refuel()
    {
        Console.WriteLine("Refueling with gasoline...");
    }
}

// Adapter Pattern
public interface IElectricVehicle
{
    void Charge();
}

public class ElectricAdapter : IElectricVehicle
{
    private readonly ICombustionEngine _combustionEngine;

    public ElectricAdapter(ICombustionEngine combustionEngine)
    {
        _combustionEngine = combustionEngine;
    }

    public void Charge()
    {
        Console.Write("Adapter converted electric charge to ");
        _combustionEngine.Refuel();
    }
}

// Flyweight Pattern
public class CarComponent
{
    private static readonly Dictionary<string, CarComponent> _components = 
        new Dictionary<string, CarComponent>();

    public string Name { get; }
    public double Cost { get; }

    private CarComponent(string name, double cost)
    {
        Name = name;
        Cost = cost;
    }

    public static CarComponent GetComponent(string name, double cost)
    {
        if (!_components.ContainsKey(name))
        {
            _components[name] = new CarComponent(name, cost);
        }
        return _components[name];
    }

    public void Display()
    {
        Console.WriteLine($"- {Name} (${Cost})");
    }

    public static void DisplayAll()
    {
        foreach (var component in _components)
        {
            Console.WriteLine($"{component.Value.Name}");
        }
    }
}

// Composite Pattern
public class Car
{
    private readonly List<CarComponent> _components = new List<CarComponent>();
    public string Model { get; }

    public Car(string model)
    {
        Model = model;
    }

    public void AddComponent(string name, double cost)
    {
        var component = CarComponent.GetComponent(name, cost);
        _components.Add(component);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Car: {Model}");
        Console.WriteLine("Components:");
        foreach (var component in _components)
        {
            component.Display();
        }
        Console.WriteLine($"Total Cost: ${GetTotalCost()}");
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (var component in _components)
        {
            total += component.Cost;
        }
        return total;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Adapter Pattern");
        IElectricVehicle adaptedGasCar = new ElectricAdapter(new GasolineCar());
        adaptedGasCar.Charge();

        Console.WriteLine("\nFlyWeight and Composite Pattern");
        var sedan = new Car("Sedan");
        sedan.AddComponent("Door", 800);
        sedan.AddComponent("Door", 800);
        sedan.AddComponent("Wheels", 400);
        sedan.AddComponent("Wheels", 400);
        sedan.DisplayInfo();

        Console.WriteLine("\nActual FlyWeight Objects");
        CarComponent.DisplayAll();
    }
}