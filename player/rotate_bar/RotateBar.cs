using Godot;
using System;

public partial class RotateBar : Node2D
{
    [Export] public float RotateScaleValue = 0f;
    public float MaxRotate = -90f;

    private AnimationPlayer _animationPlayer;
    private Panel _marker;
    private TextureRect _bar;

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

        _marker.Position = new Vector2(_marker.Position.X, (_bar.Size.Y - _marker.Size.Y) * (1 - RotateScaleValue));

        GlobalRotationDegrees = 0;
    }

}
