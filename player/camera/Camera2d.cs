using Godot;
using System;

public partial class Camera2d : Camera2D
{
    private PlayerBody _playerBody;

    public override void _Ready()
    {
        base._Ready();

        _playerBody = GetParent<PlayerBody>();

        this.MakeCurrent();

        ProcessMode = ProcessModeEnum.Disabled;

        PlayerManager.Instance.PlayerStateChanged += OnPlayerStateChanged;
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_playerBody.Velocity != Vector2.Zero)
        {
            var targetZoom = Mathf.Remap(
                _playerBody.Velocity.Length(),
                _playerBody.MaxSpeed.Length() / 2, _playerBody.MaxSpeed.Length(),
                0.2f, 0.05f);
            this.Zoom = this.Zoom.Lerp(new Vector2(targetZoom, targetZoom), 0.3f * (float)delta);
        }
    }

    public override void _ExitTree()
    {
        PlayerManager.Instance.PlayerStateChanged -= OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(EStateType stateType)
    {
        if (stateType == EStateType.Flight) ProcessMode = ProcessModeEnum.Always;
    }

}
