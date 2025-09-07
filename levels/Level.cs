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

    private Node2D _background;

    private VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        base._Ready();

        _background = GetNodeOrNull<Node2D>("Background");

        _background.Hide();

        _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

        _notifier.ScreenEntered += () => { _background.Show(); };
        _notifier.ScreenExited += () => { _background.Hide(); };

        GetNode<Area2D>("Area2D").BodyEntered += (Node2D body) => { if (body is PlayerBody playerBody) PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.BiomeEntered, (int)BiomeType); };
    }


}
