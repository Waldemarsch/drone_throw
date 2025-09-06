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

    private VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        base._Ready();

        _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

        _notifier.ScreenEntered += () => { this.ProcessMode = ProcessModeEnum.Always; this.Show(); };
        _notifier.ScreenExited += () => { this.ProcessMode = ProcessModeEnum.Disabled; this.Hide(); };

        GetNode<Area2D>("Area2D").BodyEntered += (Node2D body) => { if (body is PlayerBody playerBody) PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.BiomeEntered, (int)BiomeType); };
    }

}
