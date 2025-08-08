using Godot;
using System;

namespace DroneThrow 
{
    public partial class Entity : StaticBody2D
    {
        [Export] public EntityData EntityDataResource;

        private AudioStreamPlayer2D AudioStreamNode;
        private CpuParticles2D CpuParticles2DNode;

        public override void _Ready()
        {
            AudioStreamNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

            AudioStreamNode.Stream = EntityDataResource.EntityDeathSound;
            if (EntityDataResource.EntityDeathParticles != null)
            {
                CpuParticles2DNode = (CpuParticles2D)EntityDataResource.EntityDeathParticles.Instantiate();
                AddChild(CpuParticles2DNode);
            }
        }

    }       
}
