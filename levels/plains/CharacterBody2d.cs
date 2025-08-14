using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{

	[Export] private Camera2D Camera;

	[Export]
    private Vector2 _minZoom = new Vector2(0.1f, 0.1f);
    [Export]
    private Vector2 _maxZoom = new Vector2(2.0f, 2.0f);
	public const float Speed = 3000.0f;

	public override void _PhysicsProcess(double delta)
	{
		Velocity = Input.GetVector("left", "right", "up", "down") * Speed;

		MoveAndSlide();

		if (Input.IsActionPressed("up_arrow"))
		{
			Camera.Zoom += Vector2.One * 0.05f;
		}

		if (Input.IsActionPressed("down_arrow"))
		{
			Camera.Zoom -= Vector2.One * 0.05f;
		}

		Camera.Zoom = Camera.Zoom.Clamp(_minZoom, _maxZoom);
	}
}
