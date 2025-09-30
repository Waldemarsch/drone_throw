using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerManager : Node
{
    [Export] public PackedScene PlayerScene;

    [Export] public PlayerData PlayerDataResource;

    public PlayerBody _playerBody { get; private set; }

    public static PlayerManager Instance { get; private set; }

    public BiomeTypes CurrentBiome;

    [Signal] public delegate void CreatePlayerDataEventHandler();

    [Signal] public delegate void TransitPlayerBodyEventHandler(Node2D level, string spawnPointName);
    [Signal] public delegate void PlayerSpawnedEventHandler(PlayerBody player);

    [Signal] public delegate void PlayerStateChangedEventHandler(EStateType stateType);

    [Signal] public delegate void ScoreChangeEventHandler(int changeValue);
    [Signal] public delegate void ScoreChangedEventHandler();

    [Signal] public delegate void UpgradeApplyEventHandler(EUpgradeType upgradeType);
    [Signal] public delegate void UpgradeAppliedEventHandler(EUpgradeType upgradeType);

    [Signal] public delegate void BiomeEnteredEventHandler(BiomeTypes biomeType);

    [Signal] public delegate void PausePlayerEventHandler();
    [Signal] public delegate void UnpausePlayerEventHandler();


    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        TransitPlayerBody += OnTransitPlayerBody;

        ScoreChange += OnScoreChange;

        BiomeEntered += OnBiomeEntered;

        UpgradeApply += OnUpgradeApply;

        PausePlayer += OnPausePlayer;
        UnpausePlayer += OnUnpausePlayer;

    }

    private async void OnTransitPlayerBody(Node2D level, string spawnPointName)
    {
        if (_playerBody != null)
        {
            _playerBody?.QueueFree();
            await ToSignal(_playerBody, Node.SignalName.TreeExited);
        }
        _playerBody = (PlayerBody)PlayerScene.Instantiate();
        _playerBody.Position = level.GetNode<Marker2D>(spawnPointName).Position;
        GetTree().Root.GetNode<Node2D>("Main/World").AddChild(_playerBody);

        _playerBody.Initialize(PlayerDataResource);

        EmitSignal(SignalName.PlayerSpawned, _playerBody);
    }

    private void OnUpgradeApply(EUpgradeType upgradeType)
    {
        UpgradeData upgrade = null;
        switch (upgradeType)
        {
            case EUpgradeType.General:
                upgrade = PlayerDataResource.GeneralUpgrade;
                break;
            case EUpgradeType.Engine:
                upgrade = PlayerDataResource.EngineUpgrade;
                break;
            case EUpgradeType.Gun:
                upgrade = PlayerDataResource.GunUpgrade;
                break;
            case EUpgradeType.Gear:
                upgrade = PlayerDataResource.GearUpgrade;
                break;
            case EUpgradeType.Shield:
                upgrade = PlayerDataResource.ShieldUpgrade;
                break;
        }
        PlayerDataResource.Score -= upgrade.GetCurrentUpgradePrice();
        upgrade.CurrentUpgradeLevel += 1;
        EmitSignal(SignalName.UpgradeApplied, (int)upgradeType);
        EmitSignal(SignalName.ScoreChanged);
    }

    private void OnScoreChange(int changeValue)
    {
        PlayerDataResource.Score += changeValue;
        EmitSignal(SignalName.ScoreChanged);

    }

    private void OnBiomeEntered(BiomeTypes biomeType)
    {
        CurrentBiome = biomeType;
    }

    private void OnPausePlayer()
    {
        _playerBody.SetPhysicsProcess(false);
        _playerBody.ProcessMode = ProcessModeEnum.Disabled;
    }

    private void OnUnpausePlayer()
    {
        _playerBody.ProcessMode = ProcessModeEnum.Always;
         _playerBody.SetPhysicsProcess(true);
    }
}
