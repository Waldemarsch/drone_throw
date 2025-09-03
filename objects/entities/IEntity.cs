using Godot;
using System;

namespace DroneThrow 
{
    public interface IEntity
    {
        [Export] public EntityData EntityDataResource { get; }

        public event Action InitializeComponents;

        public Sprite2D SpriteNode { get; }
        public Area2D AreaNode { get; }
        public AudioStreamPlayer2D AudioStreamNode { get; }
        public CpuParticles2D CpuParticles2DNode { get; }

        public void _Ready();

        void Destroy();

    }       
}
