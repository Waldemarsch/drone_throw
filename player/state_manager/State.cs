using Godot;
using System;

public enum EStateType
{
    Idle,
    Begin,
    Flight,
} 

public abstract partial class State : Node
{
    [Export] public EStateType StateType { get; private set; }

    private StateManager _stateManager;

    protected Player _player;


    public override void _Ready()
    {
        _stateManager = GetParent<StateManager>();
    }

    public virtual void Enter()
    {
        _player = _stateManager.PlayerNode;
        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.PlayerStateChanged, (int)StateType);
    }

    public virtual void Exit() { }

    public virtual void PhysicsUpdate(double delta) { }

}
