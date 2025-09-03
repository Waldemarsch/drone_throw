using Godot;
using System;

namespace DroneThrow 
{
    public partial class InanimateEntity : StaticBody2D, IEntity
    {
        [Export] public EntityData EntityDataResource { get; private set; }

        public event Action InitializeComponents;

        public Area2D AreaNode { get; private set; }
        public AudioStreamPlayer2D AudioStreamNode { get; private set; }
        public CpuParticles2D CpuParticles2DNode { get; private set; }

        private Sprite2D _spriteNode;

        public override void _Ready()
        {
            AreaNode = GetNode<Area2D>("Area2D");

            _spriteNode = GetNode<Sprite2D>("Sprite2D");

            AudioStreamNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

            if (EntityDataResource.EntityDeathSound != null && EntityDataResource.EntityDeathSound.Count != 0)
            {
                Random rng = new();
                AudioStreamNode.Stream = EntityDataResource.EntityDeathSound[rng.Next(0, EntityDataResource.EntityDeathSound.Count - 1)];
            }

            CpuParticles2DNode = GetNode<CpuParticles2D>("CPUParticles2D");

            InitializeComponents.Invoke();
        }

        public void Destroy()
        {
            this.QueueFree();
        }

        public void HideVisuals()
        {
            _spriteNode.Hide();
        }
    }       
}
