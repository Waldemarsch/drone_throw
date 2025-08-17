using Godot;
using Godot.Collections;


[GlobalClass]
public partial class UpgradeData : Resource
{
    [Export] int CurrUpgradeLevel = 0;
    [Export] Array<PackedScene> UpgradeLevelsData;
}