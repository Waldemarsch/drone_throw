using Godot;
using System;

public enum EStateType
{
    Idle,
    BeginRotate,
    BeginSpeed,
    Flight,
} 

public abstract partial class State : Node
{
    [Export] public EStateType StateType { get; private set; }

    protected StateManager _stateManager;

    protected PlayerBody _playerBody;


    public override void _Ready()
    {
        _stateManager = GetParent<StateManager>();
    }
    

    public virtual void Enter()
    {
        _playerBody = _stateManager.PlayerNode;
        PlayerManager.Instance.EmitSignal(PlayerManager.SignalName.PlayerStateChanged, (int)StateType);
    }

    public virtual void Exit() { }

    public virtual void PhysicsProcess(double delta) { }

}
