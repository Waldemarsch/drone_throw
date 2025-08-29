using Godot;
using System;

public partial class StateManager : Node
{
    public State CurrentState { get; private set; }

    private Godot.Collections.Dictionary<EStateType, State> _states = new(); 

    public Player PlayerNode { get; private set; }


    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is State state)
            {
                _states[state.Type] = state;
            }
        }

        PlayerNode = GetOwner<Player>();
    }

    public void ChangeState(EStateType newState)
    {
        CurrentState.Exit();
        _states[newState].Enter();
    }
}
