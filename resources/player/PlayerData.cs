using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class PlayerData : Resource
{
    [ExportGroup("Upgrades")]
    [Export] public PackedScene GeneralUpgrade;
    [Export] public PackedScene EngineUpgrade;
    [Export] public PackedScene GunUpgrade;
    [Export] public PackedScene GearUpgrade;
    [Export] public PackedScene ShieldUpgrade;

    [ExportGroup("")]
    [Export] public int Score = 0;
}
