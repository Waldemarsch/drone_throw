using Godot;
using Godot.Collections;

namespace DroneThrow
{
    [GlobalClass]
    public partial class UpgradeData : Resource
    {
        [Export] int UpgradeLevel;
        [Export] Array<Texture2D> UpgradeLevelTextures;
        [Export] Array<AudioEffect> UpgradeLevelAudioEffects;
        [Export] Array<PackedScene> UpgradeLevelParticles;
    
        public UpgradeData() : this(0, null, null, null) { }
    
        public UpgradeData(int upgradeLevel, Array<Texture2D> upgradeLevelTextures, Array<AudioEffect> upgradeLevelAudioEffects, Array<PackedScene> upgradeLevelParticles)
        {
            UpgradeLevel = upgradeLevel;
            UpgradeLevelTextures = upgradeLevelTextures;
            UpgradeLevelAudioEffects = upgradeLevelAudioEffects;
            UpgradeLevelParticles = upgradeLevelParticles;
        }
    }
}