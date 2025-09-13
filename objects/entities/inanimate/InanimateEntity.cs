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

        private VisibleOnScreenNotifier2D _notifier;

        private Sprite2D _spriteNode;

        public override void _Ready()
        {

            _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

            _notifier.ScreenEntered += () => { _spriteNode.Show(); };
            _notifier.ScreenExited += () => { _spriteNode.Hide(); };

            AreaNode = GetNode<Area2D>("Area2D");

            _spriteNode = GetNode<Sprite2D>("Sprite2D");

            _spriteNode.Hide();

            _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

            AudioStreamNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

            AudioStreamNode.Bus = "SoundSFX";

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

        public void DisableCollision()
        {
            this.SetCollisionLayerValue(4, false);
        }
    }       
}
