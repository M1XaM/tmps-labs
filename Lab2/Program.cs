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

// Flyweight and Composite Pattern
public class CarComponent
{
    private static readonly Dictionary<string, CarComponent> _components =
        new Dictionary<string, CarComponent>();

    public string Name { get; }
    public double Cost { get; }

    private readonly List<CarComponent> _children = new List<CarComponent>();

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

    public void AddSubcomponent(string name, double cost)
    {
        var child = GetComponent(name, cost);
        _children.Add(child);
    }

    public void AddSubcomponent(CarComponent child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));
        _children.Add(child);
    }

    public void Display()
    {
        Console.WriteLine($"- {Name} (${Cost})");
    }

    public void DisplayRecursive(int indent = 0)
    {
        var pad = new string(' ', indent * 2);
        Console.WriteLine($"{pad}- {Name} (${Cost})");
        foreach (var child in _children)
        {
            child.DisplayRecursive(indent + 1);
        }
    }


    public double GetTotalCostRecursive()
    {
        double total = Cost;
        foreach (var child in _children)
        {
            total += child.GetTotalCostRecursive();
        }
        return total;
    }

    public static void DisplayAll()
    {
        foreach (var component in _components)
        {
            Console.WriteLine($"{component.Value.Name}");
        }
    }
}

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

    public void AddComponent(CarComponent component)
    {
        if (component == null) throw new ArgumentNullException(nameof(component));
        _components.Add(component);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Car: {Model}");
        Console.WriteLine("Components:");
        foreach (var component in _components)
        {
            component.DisplayRecursive(1);
        }
        Console.WriteLine($"Total Cost: ${GetTotalCost()}");
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (var component in _components)
        {
            total += component.GetTotalCostRecursive();
        }
        return total;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Adapter Pattern:");
        IElectricVehicle adaptedGasCar = new ElectricAdapter(new GasolineCar());
        adaptedGasCar.Charge();

        Console.WriteLine("\nFlyWeight and Composite Pattern:");
        var sedan = new Car("Sedan");

        sedan.AddComponent("Door", 800);
        sedan.AddComponent("Door", 800);
        sedan.AddComponent("Wheels", 400);
        sedan.AddComponent("Wheels", 400);

        var door = CarComponent.GetComponent("Door", 800);
        door.AddSubcomponent("Window", 120);
        door.AddSubcomponent("Hinge", 15);

        sedan.DisplayInfo();

        Console.WriteLine("\nActual FlyWeight Objects:");
        CarComponent.DisplayAll();
    }
}
