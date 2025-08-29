using Godot;
using System;

public partial class FlightState : State
{
    [Export] public float RotateSpeedDeg = 30.0f;

    public override void PhysicsUpdate(double delta)
    {
        if (Input.IsActionPressed("left"))
        {
            _player.RotationDegrees -= RotateSpeedDeg * (float)delta;
        }
        if (Input.IsActionPressed("right"))
        {
            _player.RotationDegrees += RotateSpeedDeg * (float)delta;
        }

        if (_player.Velocity == Vector2.Zero)
        {
            // player.ChangeState(player.GetIdleState());
        }
    }

}
