using Godot;
using System;

public partial class Camera : Camera2D
{

    private Player _player;

    public override void _Ready()
    {
        base._Ready();

        PlayerManager.Instance.PlayerSpawned += OnPlayerSpawned;

        ProcessMode = ProcessModeEnum.Disabled;

        LevelManager.Instance.LoadLevel += _ => { ProcessMode = ProcessModeEnum.Disabled; };
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        Position = _player.Position;

    }

    private void OnPlayerSpawned(Player player)
    {
        _player = player;
        ProcessMode = ProcessModeEnum.Always;
    }
}
