# SOLID
## Author: Isacescu Maxim

----

## Objectives:
- Get familiar with the Creational DPs;
- Choose a specific domain;
- Implement at least 3 CDPs for the specific domain;


## Used Design Patterns: 
- SRP – Single Responsibility Principle
- OCP – Open/Closed Principle
- DIP – Dependency Inversion Principle

## Implementation

### SRP (Single Responsibility Principle)
The VehicleController class only manages vehicle movement, not how each vehicle moves.
```csharp
class VehicleController
{
    private readonly IVehicle _vehicle;
    public void StartMoving() { _vehicle.Move(); }
}
```  

### OCP (Open/Closed Principle)
The Car and Bicycle classes implement IVehicle, allowing new vehicle types to be added without modifying existing code.
```csharp
class Car : IVehicle 
{
    public void Move()
    { 
        Console.WriteLine("The car is driving on the road."); 
    } 
}

class Bicycle : IVehicle
{
    public void Move()
    { 
        Console.WriteLine("The bicycle is pedaling along the path."); 
    }
}
```

### DIP (Dependency Inversion Principle)
VehicleController depends on the abstraction IVehicle, not concrete classes, allowing flexibility and easy extension.
```csharp
private readonly IVehicle _vehicle;
public VehicleController(IVehicle vehicle) { _vehicle = vehicle; }
```

## Results
<img src="img/result.png"/>

