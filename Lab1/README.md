# Creational Design Patterns
## Author: Isacescu Maxim

---

## Objectives:
- Get familiar with the Creational Design Patterns;
- Choose a specific domain;
- Implement at least 3 Creational Design Patterns for the specific domain;

## Used Design Patterns:
- Factory Pattern - For creating vehicles without specifying exact classes
- Singleton Pattern - For ensuring only one instance of logger exists
- Builder Pattern - For constructing complex vehicle configurations step by step

## Implementation

### Factory Pattern
Creates vehicles based on type without exposing instantiation logic.

```csharp
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
```

### Singleton Pattern
Ensures only one instance of VehicleLogger exists throughout the application.

```csharp
class VehicleLogger
{
    private static VehicleLogger _instance;
    
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
```


### Builder Pattern
Constructs vehicles through a step-by-step process with a fluent interface.

```csharp
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

IVehicle builtCar = new VehicleBuilder().SetType("car").Build();
```

## Results
<img src="img/result.png"/>