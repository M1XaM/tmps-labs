# Structural Design Patterns
## Author: Isacescu Maxim

---

## Objectives:
- Get familiar with the Structural DPs;
- Choose a specific domain;
- Implement at least 3 Structural DPs for the specific domain;

## Used Design Patterns:
- Adapter Pattern
- Composite Pattern
- Flyweight Pattern

## Implementation

### Adapter Pattern
The Adapter pattern allows incompatible interfaces to work together. The ElectricAdapter converts the ICombustionEngine interface to work with IElectricVehicle.

```csharp
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
```

### Composite Pattern
The Composite pattern treats individual objects and compositions uniformly. The Car class can contain multiple CarComponent objects and calculate the total cost.

```csharp
public class Car
{
    private readonly List<CarComponent> _components = new List<CarComponent>();
    
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
}
```

### Flyweight Pattern
The Flyweight pattern minimizes memory usage by sharing common object state. CarComponent stores shared components in a dictionary to avoid duplication.

```csharp
public class CarComponent
{
    private static readonly Dictionary<string, CarComponent> _components = 
        new Dictionary<string, CarComponent>();

    public static CarComponent GetComponent(string name, double cost)
    {
        if (!_components.ContainsKey(name))
        {
            _components[name] = new CarComponent(name, cost);
        }
        return _components[name];
    }
}
```

## Results
<img src="img/result.png" />
