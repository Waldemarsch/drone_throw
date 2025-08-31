using Godot;
using System;

public partial class Upgrade : Node2D
{
    protected PlayerBody _playerBody;

    private CollisionShape2D _collisionShape;

    public override void _Ready()
    {
        base._Ready();

        _playerBody = GetParent<PlayerBody>();

        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        _collisionShape?.Reparent(_playerBody);
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        _collisionShape?.QueueFree();
    }


}
