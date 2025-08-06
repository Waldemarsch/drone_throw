using Godot;
using Godot.Collections;

namespace DroneThrow
{
    [GlobalClass]
    public partial class UpgradeLevelData : Resource
    {
        [Export] public int UpgradeLevel;
        [Export] public SpriteFrames AnimatedSpriteFrames;
        [Export] public AudioStream UpgradeLevelAudioStream;
        [Export] public PackedScene UpgradeLevelParticles;
    
        public UpgradeLevelData() : this(0, null, null, null) { }
    
        public UpgradeLevelData(int upgradeLevel, SpriteFrames animatedSpriteFrames, AudioStream upgradeLevelAudioStream, PackedScene upgradeLevelParticles)
        {
            UpgradeLevel = upgradeLevel;
            AnimatedSpriteFrames = animatedSpriteFrames;
            UpgradeLevelAudioStream = upgradeLevelAudioStream;
            UpgradeLevelParticles = upgradeLevelParticles;
        }
    }
}