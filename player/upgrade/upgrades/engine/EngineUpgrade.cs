using DroneThrow;
using Godot;
using System;

public partial class EngineUpgrade : Node2D
{
    [Export] public EngineUpgradeLevelData EngineUpgradeLevelResource;

    [Export] public bool IsActive = false;

    private PlayerBody _playerBody;

    private float _currFuel;

    private AnimatedSprite2D _animatedSprite;

    private CpuParticles2D _particlesNode;

    private AudioStreamPlayer2D _audioPlayer;

    public override void _Ready()
    {
        _playerBody = GetParent<PlayerBody>();

        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _animatedSprite.Play("Idle");

        _particlesNode = GetNode<CpuParticles2D>("CPUParticles2D");

        _audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        _currFuel = EngineUpgradeLevelResource.FuelCapacity;

        ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void _Process(double delta)
    {
        _currFuel -= EngineUpgradeLevelResource.FuelConsumption * (float)delta;

        _playerBody.Velocity += Vector2.Right.Rotated(_playerBody.Rotation) * EngineUpgradeLevelResource.SpeedPower * (float)delta;

        if (_currFuel < 0)
        {
            DeactivateEngine();
        }
    }



    public void ActivateEngine()
    {
        if (_currFuel <= 0) return;

        IsActive = true;

        ProcessMode = ProcessModeEnum.Pausable;

        _animatedSprite.Play("Active");

        _particlesNode.Emitting = true;

        _audioPlayer.Play();
    }

    public void DeactivateEngine()
    {
        IsActive = false;

        _animatedSprite.Play("Idle");

        _particlesNode.Emitting = false;

        _audioPlayer.Stop();

        ProcessMode = ProcessModeEnum.Disabled;
        
    }
}
