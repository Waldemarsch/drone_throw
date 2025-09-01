using Godot;
using System;

public enum BiomeTypes
{
    Plains,
    City,
    Sky,
    Space
}

public partial class Level : Node2D
{
    [Export] public BiomeTypes BiomeType;

    public override void _Ready()
    {
        base._Ready();

        GetNode<Area2D>("Area2D").BodyEntered += (Node2D body) => { if (body is PlayerBody playerBody) PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.BiomeEntered, (int)BiomeType); };
    }

}
