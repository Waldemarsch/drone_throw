using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class PlayerData : Resource
{
    [ExportGroup("Upgrades")]
    [Export] public UpgradeData GeneralUpgrade;
    [Export] public UpgradeData EngineUpgrade;
    [Export] public UpgradeData GunUpgrade;
    [Export] public UpgradeData GearUpgrade;
    [Export] public UpgradeData ShieldUpgrade;

    [ExportGroup("")]
    [Export] public int Score = 0;
}
