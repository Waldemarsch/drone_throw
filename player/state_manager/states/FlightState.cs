using Godot;
using System;

public partial class FlightState : State
{
    [Export] public float RotateSpeedDeg = 30.0f;

    public override void PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("left"))
        {
            _player.RotationDegrees -= RotateSpeedDeg * (float)delta;
        }
        if (Input.IsActionPressed("right"))
        {
            _player.RotationDegrees += RotateSpeedDeg * (float)delta;
        }

        var velocity = _player.Velocity;

        velocity.Y += LevelManager.Instance.GravityForce * (float)delta;

        _player.Velocity = velocity;

        if (_player.Velocity == Vector2.Zero)
        {
            _stateManager.ChangeState(EStateType.Idle);
        }
    }

}
