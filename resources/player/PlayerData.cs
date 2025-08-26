using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class PlayerData : Resource
{
    [ExportGroup("Upgrades")]
    [Export] public int GeneralUpgradeLevel;
    [Export] public int EngineUpgradeLevel;
    [Export] public int GunUpgradeLevel;
    [Export] public int GearUpgradeLevel;
    [Export] public int ShieldUpgradeLevel;

    [ExportGroup("")]
    [Export] public int Score = 0;
}
