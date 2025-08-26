using Godot;
using Godot.Collections;


[GlobalClass]
public partial class UpgradeData : Resource
{
    [Export] Array<PackedScene> UpgradeLevelsData;
}