using Godot;
using System;

public partial class BeginRotateState : State
{

    public override void PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("begin"))
        {
            _stateManager.ChangeState(EStateType.BeginSpeed);
        }
    }

}
