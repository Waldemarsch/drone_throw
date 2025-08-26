using Godot;
using System;

public partial class FlightState : State
{
    [Export] public float RotateSpeedDeg = 30.0f;

    public override void PhysicsUpdate(double delta)
    {
        if (Input.IsActionPressed("left"))
        {
            player.RotationDegrees -= RotateSpeedDeg * (float)delta;
        }
        if (Input.IsActionPressed("right"))
        {
            player.RotationDegrees += RotateSpeedDeg * (float)delta;
        }

        if (player.Velocity == Vector2.Zero)
        {
            player.ChangeState(player.GetIdleState());
        }
    }

}
