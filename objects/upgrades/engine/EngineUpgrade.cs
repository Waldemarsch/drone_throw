using DroneThrow;
using Godot;
using System;

public partial class EngineUpgrade : Node2D
{
    [Export] public EngineUpgradeLevelData EngineUpgradeLevelResource;

    [Export] public bool IsActive = false;

    private float _currFuel;

    public override void _Ready()
    {
        _currFuel = EngineUpgradeLevelResource.FuelCapacity;

        ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void _Process(double delta)
    {
        _currFuel -= EngineUpgradeLevelResource.FuelConsumption;

        if (_currFuel < 0)
        {
            DeactivateEngine();
        }
    }



    public void ActivateEngine()
    {
        IsActive = true;

        ProcessMode = ProcessModeEnum.Pausable;
    }

    public void DeactivateEngine()
    {
        IsActive = false;

        ProcessMode = ProcessModeEnum.Disabled;
        
    }
}
