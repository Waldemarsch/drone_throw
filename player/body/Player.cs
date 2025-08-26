using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public Node2D UpgradeSceneContainer;

    [Export] public float RotateSpeed = 10.0f;

    private State _currentState;
    private IdleState _idleState;
    private BeginState _beginState;
    private FlightState _flightState;

    private UpgradeManager _upgradeManager;


    private PlayerData _playerData;

    public override void _Ready()
    {
        _idleState = GetNode<IdleState>("States/IdleState");
        _beginState = GetNode<BeginState>("States/BeginState");
        _flightState = GetNode<FlightState>("States/FlightState");

        _upgradeManager = GetNode<UpgradeManager>("UpgradeManager");

    }

    public void Initialize(PlayerData playerData)
    {
        _playerData = playerData;

        _upgradeManager.Initialize(_playerData);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState?.PhysicsUpdate(delta);

    }

    public void ChangeState(State newState)
    {
        _currentState?.Exit();

        _currentState = newState;

        _currentState.Enter();
    }

    public IdleState GetIdleState() => _idleState;
    public BeginState GetBeginState() => _beginState;
    public FlightState GetFlightState() => _flightState;

}
