using Godot;

public partial class EntityData : Resource
{
    [ExportGroup("Audio")]
    [Export] AudioStream DeathSound { get; set; } = null;
    [ExportGroup("Visual Effects")]
    [Export] NodePath DeathParticleEffect { get; set; } = null;
}