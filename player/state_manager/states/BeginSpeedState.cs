using Godot;
using System;

public partial class BeginSpeedState : State
{

    public override void PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("begin"))
        {
            _stateManager.ChangeState(EStateType.Flight);
        }
    }

}
