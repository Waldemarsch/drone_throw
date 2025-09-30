using Godot;
using System;

public partial class GeneralUpgrade : Upgrade
{
    public override void _Ready()
    {
        base._Ready();

        if (_playerBody.GetNodeOrNull<Node2D>("EngineUpgrade") != null) {
            _playerBody.GetNode<Node2D>("EngineUpgrade").Position = GetNode<Marker2D>("EngineSocket").Position;
        }

        if (_playerBody.GetNodeOrNull<Node2D>("GunUpgrade") != null) {
            _playerBody.GetNode<Node2D>("GunUpgrade").Position = GetNode<Marker2D>("GunSocket").Position;
        }
    
        if (_playerBody.GetNodeOrNull<Node2D>("GearUpgrade") != null) {
            _playerBody.GetNode<Node2D>("GearUpgrade").Position = GetNode<Marker2D>("GearSocket").Position;
        }
    
        if (_playerBody.GetNodeOrNull<Node2D>("ShieldUpgrade") != null) {
            _playerBody.GetNode<Node2D>("ShieldUpgrade").Position = GetNode<Marker2D>("ShieldSocket").Position;
        }
    }

}
