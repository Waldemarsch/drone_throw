using Godot;
using System;

public partial class PlayerBody : CharacterBody2D
{
    [Export] public Node2D UpgradeSceneContainer;

    [Export] public float RotateSpeed = 10.0f;

    private UpgradeManager _upgradeManager;
    private StateManager _stateManager;

    private SpeedBar _speedBar;
    private RotateBar _rotateBar;

    private Player _player;


    public PlayerData PlayerDataResource { get; private set; }

    [Signal] public delegate void PlayerBodyInitializedEventHandler();

    [Signal] public delegate void InitializeBodyComponentsEventHandler();

    public override void _Ready()
    {
        _player = GetOwner<Player>();

        _upgradeManager = GetNode<UpgradeManager>("UpgradeManager");

        _stateManager = GetNode<StateManager>("StateManager");

        _player.InitializePlayer += OnInitializePlayer;

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
                this.Velocity = forwardDirection * (1 - Math.Abs(0.5f - _speedBar.SpeedScaleValue)) * _speedBar.MaxSpeed;
                break;
            case EStateType.Flight:
                this.MoveAndSlide();
                break;
        }
    }


    public void OnInitializePlayer()
    {
        _speedBar = _player.GetNode<SpeedBar>("SpeedBar");
        _rotateBar = _player.GetNode<RotateBar>("RotateBar");

        PlayerDataResource = _player.PlayerDataResource;
        EmitSignal(SignalName.InitializeBodyComponents);

        ProcessMode = ProcessModeEnum.Always;

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

    public override void _ExitTree()
    {
        base._ExitTree();

        PlayerManager.Instance.PlayerStateChanged -= OnPlayerStateChanged;
    }

}
