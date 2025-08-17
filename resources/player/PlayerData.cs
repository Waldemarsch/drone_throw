using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class PlayerData : Resource
{
    [ExportGroup("Upgrades")]
    [Export] Array<PackedScene> GeneralUpgrades;

    [Export] Array<PackedScene> EngineUpgrades;
    [Export] Array<PackedScene> GunUpgrades;
    [Export] Array<PackedScene> GearUpgrades;
    [Export] Array<PackedScene> ShieldUpgrades;
}
