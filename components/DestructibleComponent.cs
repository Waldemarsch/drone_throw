using Godot;
using System;

public partial class DestructibleComponent : Node, IDestructible
{
    [ExportGroup("Audio")]
    [Export] public AudioStream DeathSound { get; set; }

    [ExportGroup("Effects")]
    [Export] public PackedScene DeathEffectScene { get; set; }

    private bool _isDead = false;

    private AudioStreamPlayer _audioPlayer;

    public override void _Ready()
    {
        base._Ready();

        _audioPlayer = new AudioStreamPlayer();
        AddChild(_audioPlayer);
    }

    public void TakeDamage()
    {
        if (!_isDead) {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        if (parent)
    }

}
