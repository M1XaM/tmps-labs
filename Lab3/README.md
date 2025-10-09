# Behavioral Design Patterns
## Author: Isacescu Maxim

----

## Objectives:
- Get familiar with the Behavioral DPs;
- Choose a specific domain;
- Implement at least 3 Behavioral DPs for the specific domain;

## Used Design Patterns: 
- Observer Pattern
- Strategy Pattern
- Command Pattern

## Implementation

### Observer Pattern
The Observer pattern defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified automatically. VehicleEventNotifier manages observers that get notified of vehicle events.
```csharp
class VehicleEventNotifier
{
    private List<IObserver> _observers = new List<IObserver>();
    
    public void Attach(IObserver observer) => _observers.Add(observer);
    
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
```

### Strategy Pattern
The Strategy pattern defines a family of algorithms, encapsulates each one, and makes them interchangeable. IBrakingBehavior allows different braking strategies to be selected at runtime.
```csharp
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
```

### Command Pattern
The Command pattern encapsulates a request as an object, thereby allowing for parameterization of clients with different requests. ICommand encapsulates vehicle operations as executable commands.
```csharp
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
```

## Results
<img src="img/result.png">
