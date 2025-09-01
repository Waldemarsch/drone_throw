using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerManager : Node
{
    [Export] public PackedScene PlayerScene;

    [Export] public PlayerData _playerData;

    private PlayerBody _playerBody;

    public static PlayerManager Instance { get; private set; }

    [Signal] public delegate void CreatePlayerDataEventHandler();

    [Signal] public delegate void TransitPlayerBodyEventHandler(Node2D level, string spawnPointName);
    [Signal] public delegate void PlayerSpawnedEventHandler(PlayerBody player);

    [Signal] public delegate void PlayerStateChangedEventHandler(EStateType stateType);

    [Signal] public delegate void ScoreChangeEventHandler(int changeValue);
    [Signal] public delegate void ScoreChangedEventHandler();


    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        CreatePlayerData += OnCreatePlayerData;

        TransitPlayerBody += OnTransitPlayerBody;

        ScoreChange += OnScoreChange;

    }

    private void OnCreatePlayerData()
    {
        _playerData = new PlayerData();
        _playerData.GeneralUpgradeLevel = 1;
        _playerData.Score = 0;
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

        _playerBody.Initialize(_playerData);

        EmitSignal(SignalName.PlayerSpawned, _playerBody);
    }

    private void OnScoreChange(int changeValue)
    {
        _playerData.Score += changeValue;

    }
}
