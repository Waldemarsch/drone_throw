using Godot;
using System;

public partial class UpgradeContainer : Node
{
    [Export] UpgradeData GeneralUpgradesResource;
    [Export] UpgradeData EngineUpgradesResource;
    [Export] UpgradeData GunUpgradesResource;
    [Export] UpgradeData GearUpgradesResource;
    [Export] UpgradeData ShieldUpgradesResource;
}
