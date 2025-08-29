using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public Node2D UpgradeSceneContainer;

    [Export] public float RotateSpeed = 10.0f;

    public State CurrentState { get; private set; }
    

    private UpgradeManager _upgradeManager;


    public PlayerData PlayerDataResource { get; private set; }

    [Signal] public delegate void InitializeComponentsEventHandler();

    public override void _Ready()
    {
        _upgradeManager = GetNode<UpgradeManager>("UpgradeManager");
    }

    public void Initialize(PlayerData playerData)
    {
        PlayerDataResource = playerData;

        EmitSignal(SignalName.InitializeComponents);
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsUpdate(delta);

    }

    public void ChangeState(State newState)
    {
        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }
}
