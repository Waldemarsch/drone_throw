using DroneThrow;
using Godot;
using System;

public partial class DestructibleComponent : Node
{
    private Entity _entityOwner;

    private bool _isDead = false;

    public override void _Ready()
    {
        base._Ready();

        _entityOwner = GetParent<Entity>();

        _entityOwner.InitializeComponents += OnInitializeComponents;
    }

    private void OnInitializeComponents()
    {
        _entityOwner.GetNode<Area2D>("Area2D").BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is PlayerBody playerBody)
        {
            Die();
        }
    }

    private async void Die()
    {
        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.ScoreChange, _entityOwner.ScoreValue);
        if (_isDead) { _entityOwner.QueueFree(); return; }
        ;
        _isDead = true;

        if (_entityOwner.CpuParticles2DNode != null)
        {
            _entityOwner.SpriteNode.Hide();
            _entityOwner.CpuParticles2DNode.Emitting = true;

        }

        if (_entityOwner.AudioStreamNode != null)
        {
            _entityOwner.AudioStreamNode.Play();
        }

        await ToSignal(_entityOwner.AudioStreamNode, AudioStreamPlayer2D.SignalName.Finished);
        await ToSignal(_entityOwner.CpuParticles2DNode, CpuParticles2D.SignalName.Finished);
        _entityOwner.QueueFree();
    }

}
