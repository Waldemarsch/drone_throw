using Godot;
using System;

public partial class Camera : Camera2D
{

    private Player _playerBody;

    public override void _Ready()
    {
        base._Ready();

        PlayerManager.Instance.TransitPlayerBodyStarted += OnDisableCamera;
        PlayerManager.Instance.TransitPlayerBodyFinished += OnEnableCamera;

        ProcessMode = ProcessModeEnum.Disabled;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        Position = _playerBody.Position;
    }

    private void OnEnableCamera(Player playerBody)
    {
        _playerBody = playerBody;
        ProcessMode = ProcessModeEnum.Always;
        Position = _playerBody.Position;
        PositionSmoothingEnabled = true;
    }

    private void OnDisableCamera()
    {
        _playerBody = null;
        PositionSmoothingEnabled = false;
        ProcessMode = ProcessModeEnum.Disabled;
    }

}
