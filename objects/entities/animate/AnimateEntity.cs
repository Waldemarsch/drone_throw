using Godot;
using System;

namespace DroneThrow 
{
    public partial class AnimateEntity : CharacterBody2D, IEntity
    {
        [Export] public EntityData EntityDataResource { get; private set; }
        [Export] private float _speed = 100.0f;

        public event Action InitializeComponents;

        public Sprite2D SpriteNode { get; private set; }
        public Area2D AreaNode { get; private set; }
        public AudioStreamPlayer2D AudioStreamNode { get; private set; }
        public CpuParticles2D CpuParticles2DNode { get; private set; }

        // 1 для "вправо", -1 для "влево"
        private int _direction = 1;

        private AnimatedSprite2D _animatedSprite;

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

            _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            InitializeComponents.Invoke();

            // 1. Выбираем случайное направление при спауне
            ChooseRandomDirection();

            // 2. Запускаем анимацию
            _animatedSprite.Play("walk");
        }

        public override void _PhysicsProcess(double delta)
        {
            var velocity = this.Velocity;

            // 3. Постоянно применяем скорость в выбранном направлении
            velocity.X = _direction * _speed;

            this.Velocity = velocity;
            MoveAndSlide();
        }

        private void ChooseRandomDirection()
        {
            // GD.Randf() возвращает случайное число от 0.0 до 1.0
            // Это простой способ выбрать 1 или -1 с шансом 50/50
            _direction = (GD.Randf() > 0.5f) ? 1 : -1;

            // 4. Отражаем спрайт, если движемся влево
            _animatedSprite.FlipH = (_direction == -1);
        }

        public void Destroy()
        {
            this.QueueFree();
        }
    }
}
