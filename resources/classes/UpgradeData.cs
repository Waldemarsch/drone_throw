using Godot;
using Godot.Collections;

namespace DroneThrow
{
    [GlobalClass]
    public partial class UpgradeData : Resource
    {
        [Export] Array<UpgradeLevelData> UpgradeLevelsData;
    
        public UpgradeData() : this(null) { }
    
        public UpgradeData(Array<UpgradeLevelData> upgradeLevelsData)
        {
            UpgradeLevelsData = upgradeLevelsData;
        }
    }
}