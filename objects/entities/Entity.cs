using Godot;
using System;

namespace DroneThrow 
{
    public partial class Entity : StaticBody2D
    {
        [Export] public EntityData EntityDataResource;

        private Sprite2D EntitySprite;
        private AudioStreamPlayer2D AudioStreamNode;
        private CpuParticles2D CpuParticles2DNode;

        public override void _Ready()
        {
            EntitySprite.Texture = EntityDataResource.EntityTexture;
            AudioStreamNode.Stream = EntityDataResource.EntityDeathSound;
            CpuParticles2DNode = (CpuParticles2D)EntityDataResource.EntityDeathParticles.Instantiate();
            AddChild(CpuParticles2DNode);
        }

    }       
}
