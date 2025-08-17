using Godot;
using Godot.Collections;


public partial class UpgradeLevelData : Resource
{
    [Export] public int UpgradeLevel;
    [Export] public SpriteFrames AnimatedSpriteFrames;
    [Export] public AudioStream UpgradeLevelAudioStream;
}