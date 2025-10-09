using System;
using System.Collections.Generic;

// Observer Pattern - Vehicle Event Notifications
interface IObserver
{
    void Update(string eventType);
}

class VehicleEventNotifier
{
    private List<IObserver> _observers = new List<IObserver>();
    
    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);
    
    public void Notify(string eventType)
    {
        foreach (var observer in _observers)
            observer.Update(eventType);
    }
}

class Dashboard : IObserver
{
    public void Update(string eventType)
    {
        Console.WriteLine($"Dashboard: Vehicle {eventType}");
    }
}

// Strategy Pattern - Braking Behavior
interface IBrakingBehavior
{
    void Brake();
}

class StandardBraking : IBrakingBehavior
{
    public void Brake() => Console.WriteLine("Standard braking with 70% force");
}

class ABSBraking : IBrakingBehavior
{
    public void Brake() => Console.WriteLine("ABS braking with pulsating brakes");
}

// Command Pattern - Vehicle Commands
interface ICommand
{
    void Execute();
}

class IgnitionCommand : ICommand
{
    private VehicleEventNotifier _notifier;
    
    public IgnitionCommand(VehicleEventNotifier notifier) => _notifier = notifier;
    
    public void Execute()
    {
        Console.WriteLine("Starting ignition...");
        _notifier.Notify("ignition started");
    }
}

class BrakeCommand : ICommand
{
    private IBrakingBehavior _brakingBehavior;
    
    public BrakeCommand(IBrakingBehavior brakingBehavior) => _brakingBehavior = brakingBehavior;
    
    public void Execute()
    {
        Console.Write("Activating brakes: ");
        _brakingBehavior.Brake();
    }
}

// Vehicle Class
class Vehicle
{
    private VehicleEventNotifier _notifier = new VehicleEventNotifier();
    private IBrakingBehavior _brakingBehavior;
    
    public Vehicle(IBrakingBehavior brakingBehavior)
    {
        _brakingBehavior = brakingBehavior;
        // Add default observer
        _notifier.Attach(new Dashboard());
    }
    
    public void AddObserver(IObserver observer) => _notifier.Attach(observer);
    
    public void StartIgnition()
    {
        new IgnitionCommand(_notifier).Execute();
    }
    
    public void ApplyBrakes()
    {
        new BrakeCommand(_brakingBehavior).Execute();
    }
    
    public void SetBrakingBehavior(IBrakingBehavior brakingBehavior)
    {
        _brakingBehavior = brakingBehavior;
    }
}

// Demo
class Program
{
    static void Main()
    {
        // Create vehicles with different braking strategies
        Vehicle sedan = new Vehicle(new StandardBraking());
        Vehicle suv = new Vehicle(new ABSBraking());
        
        // Add additional observer to SUV
        suv.AddObserver(new Dashboard()); // Second dashboard

        Console.WriteLine("Sedan operations:");
        sedan.StartIgnition();
        sedan.ApplyBrakes();
        
        Console.WriteLine("\nSUV operations:");
        suv.StartIgnition();
        suv.ApplyBrakes();
        
        // Change braking behavior at runtime
        Console.WriteLine("\nSedan with ABS braking:");
        sedan.SetBrakingBehavior(new ABSBraking());
        sedan.ApplyBrakes();
    }
}