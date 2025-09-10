using Godot;
using System;

public partial class PlayerBody : CharacterBody2D
{
    [Export] public Node2D UpgradeSceneContainer;

    [Export] public float RotateSpeed = 10f;
    [Export] public Vector2 MaxSpeed = new (2000f, 2000f);

    [Export] public float AirFriction = 30f;
    [Export] public float GroundFriction = 200f;

    [Export] public float GravityForce = 100f;

    private UpgradeManager _upgradeManager;
    private StateManager _stateManager;

    private SpeedBar _speedBar;
    private RotateBar _rotateBar;

    private RemoteTransform2D _remoteTransform;

    public PlayerData PlayerDataResource { get; private set; }

    [Signal] public delegate void PlayerBodyInitializedEventHandler();

    [Signal] public delegate void InitializeBodyComponentsEventHandler();

    public override void _Ready()
    {
        _remoteTransform = GetNode<RemoteTransform2D>("RemoteTransform2D");

        _upgradeManager = GetNode<UpgradeManager>("UpgradeManager");

        _stateManager = GetNode<StateManager>("StateManager");

        PlayerManager.Instance.PlayerStateChanged += OnPlayerStateChanged;

        ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        switch (_stateManager.CurrentState.StateType)
        {
            case EStateType.BeginRotate:
                this.RotationDegrees = _rotateBar.RotateScaleValue * _rotateBar.MaxRotate;
                break;
            case EStateType.BeginSpeed:
                Vector2 forwardDirection = Vector2.Right.Rotated(this.Rotation);
                this.Velocity = forwardDirection * (1 - Math.Abs(0.5f - _speedBar.SpeedScaleValue)) * MaxSpeed;
                break;
            case EStateType.Flight:
                break;
        }
    }


    public void Initialize(PlayerData playerData)
    {
        PlayerDataResource = playerData;

        _speedBar = GetNode<SpeedBar>("Node/Node2D/SpeedBar");
        _rotateBar = GetNode<RotateBar>("Node/Node2D/RotateBar");

        EmitSignal(SignalName.InitializeBodyComponents);

        ProcessMode = ProcessModeEnum.Pausable;

        EmitSignal(SignalName.PlayerBodyInitialized);
    }

    private void OnPlayerStateChanged(EStateType stateType)
    {
        switch (stateType)
        {
            case EStateType.BeginRotate:
                _speedBar.Position = _upgradeManager.GetUpgradeScene(EUpgradeType.General).GetNode<Marker2D>("SpeedBarSocket").Position;
                _rotateBar.Position = _upgradeManager.GetUpgradeScene(EUpgradeType.General).GetNode<Marker2D>("RotateBarSocket").Position;

                _rotateBar.Show();
                _rotateBar.GetNode<AnimationPlayer>("AnimationPlayer").Play("active");

                break;

            case EStateType.BeginSpeed:
                _rotateBar.QueueFree();

                _speedBar.Show();
                _speedBar.GetNode<AnimationPlayer>("AnimationPlayer").Play("active");

                break;

            case EStateType.Flight:
                _speedBar.QueueFree();

                break;
        }
    }

    public void RequestEngineActivation()
    {
        _upgradeManager.ActivateEngine();
    }

    public void RequestEngineDeactivation()
    {
        _upgradeManager.DeactivateEngine();
    }

    public override void _ExitTree()
    {
        PlayerManager.Instance.PlayerStateChanged -= OnPlayerStateChanged;
        base._ExitTree();
    }

}
