using Godot;
using System;

namespace DroneThrow 
{
    public partial class AnimateEntity : CharacterBody2D, IEntity
    {
        [Export] public EntityData EntityDataResource { get; private set; }
        [Export] private float _speed = 100.0f;

        public event Action InitializeComponents;

        public Area2D AreaNode { get; private set; }
        public AudioStreamPlayer2D AudioStreamNode { get; private set; }
        public CpuParticles2D CpuParticles2DNode { get; private set; }

        // 1 для "вправо", -1 для "влево"
        private int _direction = 1;

        private Timer _timer;

        private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;

        private AnimatedSprite2D _animatedSprite;
        
        [Export] private bool _allowedToSwitchDirection = true;

        public override void _Ready()
        {
            _timer = GetNode<Timer>("Timer");

            _visibleOnScreenNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");

            _visibleOnScreenNotifier.Show();

            AreaNode = GetNode<Area2D>("Area2D");

            _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            _animatedSprite.Hide();
            _animatedSprite.Stop();

            AudioStreamNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

            if (EntityDataResource.EntityDeathSound != null && EntityDataResource.EntityDeathSound.Count != 0)
            {
                Random rng = new();
                AudioStreamNode.Stream = EntityDataResource.EntityDeathSound[rng.Next(0, EntityDataResource.EntityDeathSound.Count - 1)];
            }

            CpuParticles2DNode = GetNode<CpuParticles2D>("CPUParticles2D");

            InitializeComponents.Invoke();

            ChooseRandomDirection();

            _visibleOnScreenNotifier.ScreenEntered += () =>
            {
                _allowedToSwitchDirection = false;
                _animatedSprite.Play("default");
                _animatedSprite.Show();

                ProcessMode = ProcessModeEnum.Always;
            };
            _visibleOnScreenNotifier.ScreenExited += () =>
            {
                _allowedToSwitchDirection = true;
                _animatedSprite.Stop();
                _animatedSprite.Hide();

                ProcessMode = ProcessModeEnum.Disabled;
            };

            _timer.Start();

            _timer.Timeout += ChooseRandomDirection;

            ProcessMode = ProcessModeEnum.Disabled;
        }

        public override void _PhysicsProcess(double delta)
        {
            var velocity = this.Velocity;

            velocity.X = _direction * _speed;

            this.Velocity = velocity;

            MoveAndSlide();
        }

        private void ChooseRandomDirection()
        {
            if (_allowedToSwitchDirection != true) return;

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

        public void HideVisuals()
        {
            _animatedSprite.Hide();
        }
    }
}
