using Godot;
using System;

public partial class DestructibleComponent : Node, IDestructible
{
    [ExportGroup("Audio")]
    [Export] public AudioStream DeathSound { get; set; }

    [ExportGroup("Effects")]
    [Export] public PackedScene DeathEffectScene { get; set; }

    private bool _isDead = false;

    public void TriggerDestruction()
    {
        Die();
    }


    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        if (DeathEffectScene != null)
        {
            Node2D parent = (Node2D)GetParent();
            Node2D deathEffectSceneInstance = (Node2D)DeathEffectScene.Instantiate();
            deathEffectSceneInstance.GlobalPosition = parent.GlobalPosition;
            GetTree().Root.AddChild(deathEffectSceneInstance);

        }

        if (DeathSound != null)
        {
            var deathAudioPlayer = new AudioStreamPlayer();
            deathAudioPlayer.Stream = DeathSound;
            GetTree().Root.AddChild(deathAudioPlayer);
            deathAudioPlayer.Play();
            deathAudioPlayer.Finished += () => deathAudioPlayer.QueueFree();
        }

        GetParent().QueueFree();
    }

}
