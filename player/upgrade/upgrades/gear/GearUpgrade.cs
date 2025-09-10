using Godot;
using System;

public partial class GearUpgrade : Upgrade
{
    [Export] public GearUpgradeLevelData GearUpgradeLevelDataResource;

    public override void _Ready()
    {
        base._Ready();

        _playerBody.GroundFriction *= 0.8f;
    }

}
