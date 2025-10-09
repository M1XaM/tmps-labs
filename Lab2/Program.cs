using System;
using System.Collections.Generic;

// Adapter Pattern
// Existing interface
public interface IElectricVehicle
{
    void Charge();
}

// New interface that needs adapting
public interface ICombustionEngine
{
    void Refuel();
}

// Adaptee class
public class GasolineCar : ICombustionEngine
{
    public void Refuel()
    {
        Console.WriteLine("Refueling with gasoline...");
    }
}

// Adapter
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

// Composite Pattern
public abstract class VehicleComponent
{
    public abstract void DisplayInfo(int depth = 0);
}

// Leaf class
public class VehiclePart : VehicleComponent
{
    private readonly string _name;

    public VehiclePart(string name)
    {
        _name = name;
    }

    public override void DisplayInfo(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" Part: {_name}");
    }
}

// Composite class
public class VehicleAssembly : VehicleComponent
{
    private readonly List<VehicleComponent> _children = new List<VehicleComponent>();
    private readonly string _name;

    public VehicleAssembly(string name)
    {
        _name = name;
    }

    public void Add(VehicleComponent component)
    {
        _children.Add(component);
    }

    public override void DisplayInfo(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" Assembly: {_name}");
        foreach (var component in _children)
        {
            component.DisplayInfo(depth + 2);
        }
    }
}

// Decorator Pattern
public abstract class Vehicle
{
    public abstract string GetDescription();
    public abstract double GetCost();
}

// Concrete component
public class BasicCar : Vehicle
{
    public override string GetDescription()
    {
        return "Basic Car";
    }

    public override double GetCost()
    {
        return 20000;
    }
}

// Decorator base
public abstract class VehicleDecorator : Vehicle
{
    protected Vehicle _vehicle;

    protected VehicleDecorator(Vehicle vehicle)
    {
        _vehicle = vehicle;
    }

    public override string GetDescription()
    {
        return _vehicle.GetDescription();
    }

    public override double GetCost()
    {
        return _vehicle.GetCost();
    }
}

// Concrete decorators
public class SunroofDecorator : VehicleDecorator
{
    public SunroofDecorator(Vehicle vehicle) : base(vehicle) { }

    public override string GetDescription()
    {
        return _vehicle.GetDescription() + " + Sunroof";
    }

    public override double GetCost()
    {
        return _vehicle.GetCost() + 1500;
    }
}

public class PremiumSoundDecorator : VehicleDecorator
{
    public PremiumSoundDecorator(Vehicle vehicle) : base(vehicle) { }

    public override string GetDescription()
    {
        return _vehicle.GetDescription() + " + Premium Sound";
    }

    public override double GetCost()
    {
        return _vehicle.GetCost() + 800;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ADAPTER PATTERN DEMO:");
        IElectricVehicle adaptedGasCar = new ElectricAdapter(new GasolineCar());
        adaptedGasCar.Charge();

        Console.WriteLine("\nCOMPOSITE PATTERN DEMO:");
        var engine = new VehicleAssembly("Engine");
        engine.Add(new VehiclePart("Pistons"));
        engine.Add(new VehiclePart("Crankshaft"));

        var chassis = new VehicleAssembly("Chassis");
        chassis.Add(new VehiclePart("Frame"));
        chassis.Add(engine);

        chassis.DisplayInfo();

        Console.WriteLine("\nDECORATOR PATTERN DEMO:");
        Vehicle myCar = new BasicCar();
        myCar = new SunroofDecorator(myCar);
        myCar = new PremiumSoundDecorator(myCar);

        Console.WriteLine($"{myCar.GetDescription()} costs ${myCar.GetCost()}");

        Console.ReadKey();
    }
}