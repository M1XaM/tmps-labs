using System;
using System.Collections.Generic;

interface IDriveMode
{
    void Accelerate();
}

class EcoMode : IDriveMode
{
    public void Accelerate() => Console.WriteLine("Eco: Slow acceleration");
}

class SportMode : IDriveMode
{
    public void Accelerate() => Console.WriteLine("Sport: Fast acceleration");
}

interface IWatcher
{
    void Notify(string message);
}

class Dashboard : IWatcher
{
    public void Notify(string message) => Console.WriteLine($"Dashboard: {message}");
}

class Telemetry : IWatcher
{
    public void Notify(string message) => Console.WriteLine($"Telemetry: {message}");
}

class LoggingSystem : IWatcher
{
    public void Notify(string message) => Console.WriteLine($"Logging: {message}");
}

class Notifier
{
    private List<IWatcher> _watchers = new List<IWatcher>();
    
    public void AddWatcher(IWatcher watcher) => _watchers.Add(watcher);
    public void RemoveWatcher(IWatcher watcher) => _watchers.Remove(watcher);
    
    public void SendMessage(string message)
    {
        foreach (var watcher in _watchers)
            watcher.Notify(message);
    }
}

interface ICarState
{
    void Start(Car car);
    void Stop(Car car);
}

class ParkedState : ICarState
{
    public void Start(Car car)
    {
        Console.WriteLine("Starting from parked...");
        car.SetState(new RunningState());
    }
    
    public void Stop(Car car)
    {
        Console.WriteLine("Already parked!");
    }
}

class RunningState : ICarState
{
    public void Start(Car car)
    {
        Console.WriteLine("Already running!");
    }
    
    public void Stop(Car car)
    {
        Console.WriteLine("Stopping...");
        car.SetState(new ParkedState());
    }
}

class Car
{
    private IDriveMode _driveMode;
    private ICarState _state;
    private Notifier _notifier = new Notifier();
    
    public Car(IDriveMode driveMode, ICarState carState)
    {
        _driveMode = driveMode;
        _state = carState;
    }
    
    public void AddWatcher(IWatcher watcher) => _notifier.AddWatcher(watcher);
    public void RemoveWatcher(IWatcher watcher) => _notifier.RemoveWatcher(watcher);
    
    public void SetDriveMode(IDriveMode mode)
    {
        _driveMode = mode;
        _notifier.SendMessage($"Mode: {mode.GetType().Name}");
    }
    
    public void SetState(ICarState state) => _state = state;
    
    public void Start()
    {
        _notifier.SendMessage("Start requested");
        _state.Start(this);
    }
    
    public void Stop()
    {
        _notifier.SendMessage("Stop requested");
        _state.Stop(this);
    }
    
    public void Accelerate()
    {
        _notifier.SendMessage("Accelerating");
        _driveMode.Accelerate();
    }
}

class Program
{
    static void Main()
    {     
        var car = new Car(new EcoMode(), new ParkedState());

        Console.WriteLine("Observer Pattern");
        car.AddWatcher(new Dashboard());
        car.AddWatcher(new Telemetry());
        
        Console.WriteLine("State Pattern");
        car.Start();
        car.Start();
        car.Stop();
        car.Stop();
        
        Console.WriteLine("\nStrategy Pattern");
        car.Accelerate();
        car.SetDriveMode(new SportMode());
        car.Accelerate();
    }
}