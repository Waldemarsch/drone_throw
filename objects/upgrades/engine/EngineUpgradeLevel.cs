using DroneThrow;
using Godot;
using System;

public partial class EngineUpgradeLevel : StaticBody2D
{
    [ExportGroup("Data")]
    [Export] UpgradeLevelData UpgradeLevelDataResource;


    private AnimatedSprite2D AnimatedSpriteNode;
    private CpuParticles2D CpuParticles2DNode;

    public override void _Ready()
    {
        base._Ready();

        AnimatedSpriteNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        AnimatedSpriteNode.SpriteFrames = UpgradeLevelDataResource.AnimatedSpriteFrames;
        AnimatedSpriteNode.Animation = "moving";

        CpuParticles2DNode = (CpuParticles2D)UpgradeLevelDataResource.UpgradeLevelParticles.Instantiate();
        AddChild(CpuParticles2DNode);
    }

    
}
