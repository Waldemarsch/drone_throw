using Godot;
using System;

public partial class IdleState : State
{
    public override void PhysicsProcess(double delta)
    {
        base.PhysicsProcess(delta);

        if (Input.IsActionJustPressed("begin"))
        {
            _stateManager.ChangeState(EStateType.BeginRotate);
        }
    }

}
