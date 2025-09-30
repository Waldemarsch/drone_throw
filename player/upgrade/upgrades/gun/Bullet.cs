using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export]
    private float _speed = 10000.0f;

    private Vector2 _direction = Vector2.Zero;

    public override void _Ready()
    {
        // Подписываемся на сигнал столкновения
        this.AreaEntered += _ => QueueFree();
    }
    
    public void Launch(Vector2 targetPosition)
    {
        // Вычисляем и сохраняем направление
        _direction = (targetPosition - this.GlobalPosition).Normalized();
    }

    public override void _PhysicsProcess(double delta)
    {
        // Двигаем пулю в заданном направлении
        this.Position += _direction * _speed * (float)delta;
    }
}
