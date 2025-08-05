using Godot;
using Godot.Collections;

public partial class UpgradeData : Resource
{
    [Export] float UpgradeType;
    [Export] Array<Texture2D> UpgradeLevelTextures;
    [Export] Array<AudioEffect> UpgradeLevelAudioEffects;
    [Export] Array<NodePath> UpgradeLevelParticles;
}