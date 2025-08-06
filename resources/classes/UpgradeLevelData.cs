using Godot;
using Godot.Collections;

namespace DroneThrow
{
    [GlobalClass]
    public partial class UpgradeLevelData : Resource
    {
        [Export] public int UpgradeLevel;
        [Export] public SpriteFrames AnimatedSpriteFrames;
        [Export] public AudioEffect UpgradeLevelAudioEffects;
        [Export] public PackedScene UpgradeLevelParticles;
    
        public UpgradeLevelData() : this(0, null, null, null) { }
    
        public UpgradeLevelData(int upgradeLevel, SpriteFrames animatedSpriteFrames, AudioEffect upgradeLevelAudioEffects, PackedScene upgradeLevelParticles)
        {
            UpgradeLevel = upgradeLevel;
            AnimatedSpriteFrames = animatedSpriteFrames;
            UpgradeLevelAudioEffects = upgradeLevelAudioEffects;
            UpgradeLevelParticles = upgradeLevelParticles;
        }
    }
}