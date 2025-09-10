using DroneThrow;
using Godot;
using System;

public partial class DestructibleComponent : Node
{
    private IEntity _entityOwner;

    private bool _isDead = false;

    public override void _Ready()
    {
        base._Ready();

        _entityOwner = GetParent<IEntity>();

        _entityOwner.InitializeComponents += OnInitializeComponents;
    }

    private void OnInitializeComponents()
    {
        _entityOwner.AreaNode.BodyEntered += OnBodyEntered;

        _entityOwner.AreaNode.AreaEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is PlayerBody || body is Bullet)
        {
            Die();
        }
    }

    private async void Die()
    {
        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.ScoreChange, _entityOwner.EntityDataResource.ScoreValue);
        if (_isDead) { _entityOwner.Destroy(); return; }
        _isDead = true;

        _entityOwner.DisableCollision();

        if (_entityOwner.CpuParticles2DNode != null)
        {
            _entityOwner.HideVisuals();
            _entityOwner.CpuParticles2DNode.Emitting = true;

        }

        if (_entityOwner.AudioStreamNode != null)
        {
            _entityOwner.AudioStreamNode.Play();
        }

        await ToSignal(_entityOwner.AudioStreamNode, AudioStreamPlayer2D.SignalName.Finished);
        await ToSignal(_entityOwner.CpuParticles2DNode, CpuParticles2D.SignalName.Finished);
        _entityOwner.Destroy();
    }

}
