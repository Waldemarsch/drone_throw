using Godot;
using System;

public partial class FlightState : State
{
    [Export] public float RotateSpeedDeg = 30.0f;

    public override void PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("left"))
        {
            _playerBody.RotationDegrees -= RotateSpeedDeg * (float)delta;
        }
        if (Input.IsActionPressed("right"))
        {
            _playerBody.RotationDegrees += RotateSpeedDeg * (float)delta;
        }

        var velocity = _playerBody.Velocity;

        if (_playerBody.IsOnFloor()) velocity.X = Mathf.MoveToward(velocity.X, 0, _playerBody.GroundFriction * (float)delta);
        else velocity.X = Mathf.MoveToward(velocity.X, 0, _playerBody.AirFriction * (float)delta);

        if (!_playerBody.IsOnFloor()) velocity.Y += _playerBody.GravityForce * (float)delta;
        else velocity.Y = 0.01f;

        _playerBody.Velocity = velocity;

        if (_playerBody.IsOnFloor() && Mathf.IsZeroApprox(_playerBody.Velocity.X))
        {
            ToUpgradeMenu();

            _stateManager.ChangeState(EStateType.None);
        }

        _playerBody.MoveAndSlide();

    }

    private async void ToUpgradeMenu()
    {
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Change);

        await ToSignal(SceneManager.Instance, SceneManager.SignalName.AllowSceneTransition);

        UIManager.Instance.EmitSignal(UIManager.SignalName.HideUIElement, "GameInterface");
        UIManager.Instance.EmitSignal(UIManager.SignalName.AddUIElement, "UpgradeMenu");
        UIManager.Instance.EmitSignal(UIManager.SignalName.ShowUIElement, "UpgradeMenu");
    }

}
