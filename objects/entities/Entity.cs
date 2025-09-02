using Godot;
using System;

namespace DroneThrow 
{
    public partial class Entity : StaticBody2D
    {
        [Export] public EntityData EntityDataResource;

        [Export] public int ScoreValue = 0;

        [Signal] public delegate void InitializeComponentsEventHandler();

        public Sprite2D SpriteNode;
        public AudioStreamPlayer2D AudioStreamNode;
        public CpuParticles2D CpuParticles2DNode; 

        public override void _Ready()
        {
            SpriteNode = GetNode<Sprite2D>("Sprite2D");

            AudioStreamNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

            if (EntityDataResource.EntityDeathSound != null && EntityDataResource.EntityDeathSound.Count != 0)
            {
                Random rng = new();
                AudioStreamNode.Stream = EntityDataResource.EntityDeathSound[rng.Next(0, EntityDataResource.EntityDeathSound.Count - 1)];
            }

            CpuParticles2DNode = GetNode<CpuParticles2D>("CPUParticles2D");

            EmitSignal(SignalName.InitializeComponents);
        }

    }       
}
