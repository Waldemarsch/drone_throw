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

    private void Die()
    {
        if (_isDead) { _entityOwner.QueueFree();  return; };
        _isDead = true;

        if (_entityOwner.CpuParticles2DNode != null)
        {
            _entityOwner.CpuParticles2DNode.Emitting = true;
            _entityOwner.CpuParticles2DNode.Finished += _entityOwner.Hide;

        }

        if (_entityOwner.AudioStreamNode != null)
        {

            _entityOwner.AudioStreamNode.Play();
            _entityOwner.AudioStreamNode.Finished += _entityOwner.QueueFree;
        }

        _entityOwner.QueueFree();
    }

}
