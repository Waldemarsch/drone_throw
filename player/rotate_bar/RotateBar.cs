using Godot;
using System;

public partial class RotateBar : Node2D
{
    [Export] public float SpeedScaleValue = 0f;

    private AnimationPlayer _animationPlayer;
    private Panel _marker;
    private TextureRect _bar;

    private float maxSpeed = 2000f;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _marker = GetNode<Panel>("Panel/Bar/Marker");
        _bar = GetNode<TextureRect>("Panel/Bar");

        // ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        _marker.Position = new Vector2(_marker.Position.X, (_bar.Size.Y - _marker.Size.Y) * SpeedScaleValue);
    }

}
