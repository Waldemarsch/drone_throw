using Godot;

[GlobalClass] public partial class EntityData : Resource
{
    [Export] public Texture2D EntityTexture;
    [Export] public SpriteFrames SpriteFramesRes;
    [Export] public AudioStream EntityDeathSound;
    [Export] public PackedScene EntityDeathParticles;
}