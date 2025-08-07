using Godot;

public partial class EntityData : Resource
{
    [Export] public int UpgradeLevel;
    [Export] public Texture2D EntityTexture;
    [Export] public AudioStream EntityDeathSound;
    [Export] public PackedScene EntityDeathParticles;
}