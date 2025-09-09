using Godot;
using Godot.Collections;

namespace DroneThrow
{
    [GlobalClass]
    public partial class EngineUpgradeLevelData : UpgradeLevelData
    {
        [Export] public float SpeedPower;
        [Export] public float FuelCapacity = 100;
        [Export] public float FuelConsumption;
    }
}