using Godot;
using System;

public partial class PlayerManager : Node
{
    [Export] public PackedScene PlayerScene;

    private PlayerData _playerData;
    private Player _player;

    public static PlayerManager Instance { get; private set; }

    [Signal] public delegate void CreatePlayerDataEventHandler();

    [Signal] public delegate void TransitPlayerBodyEventHandler(Node2D level, string spawnPointName);
    [Signal] public delegate void PlayerSpawnedEventHandler(Player player);

    [Signal] public delegate void PlayerStateChangedEventHandler(EStateType stateType);


    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        CreatePlayerData += OnCreatePlayerData;

        TransitPlayerBody += OnTransitPlayerBody;

    }

    private void OnCreatePlayerData()
    {
        _playerData = new PlayerData();
        _playerData.GeneralUpgradeLevel = 1;
    }

    private void OnTransitPlayerBody(Node2D level, string spawnPointName)
    {
        _player?.QueueFree();
        _player = (Player)PlayerScene.Instantiate();
        _player.Position = level.GetNode<Marker2D>(spawnPointName).Position;
        GetTree().Root.GetNode<Node2D>("Main/World").AddChild(_player);

        _player.Initialize(_playerData);

        EmitSignal(SignalName.PlayerSpawned, _player);
    }
}
