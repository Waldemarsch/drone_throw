using Godot;
using System;

public abstract partial class State : Node
{
    protected Player player;

    public override void _Ready()
    {
        player = GetOwner<Player>();
    }

    public virtual void Enter() { }

    public virtual void Exit() { }
    
    public virtual void PhysicsUpdate(double delta) { }

}
