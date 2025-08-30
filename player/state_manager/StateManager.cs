using Godot;
using System;

public partial class StateManager : Node
{
    public State CurrentState { get; private set; }

    private Godot.Collections.Dictionary<EStateType, State> _states = new(); 

    public PlayerBody PlayerNode { get; private set; }


    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is State state)
            {
                _states[state.StateType] = state;
            }
        }

        PlayerNode = GetOwner<PlayerBody>();

        PlayerNode.InitializeBodyComponents += OnInitializeBodyComponents;
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsProcess(delta);
    }


    private void OnInitializeBodyComponents()
    {
        ChangeState(EStateType.Idle);
    }

    public void ChangeState(EStateType newState)
    {
        CurrentState?.Exit();
        CurrentState = _states[newState];
        CurrentState.Enter();
    }
}
