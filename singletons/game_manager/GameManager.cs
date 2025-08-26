using Godot;
using System;

public partial class GameManager : Node
{
    [Export] public PackedScene PlayerBodyScene;

    private PlayerData _playerData;
    private Player _playerBody;

    public static GameManager Instance { get; private set; }

    [Signal] public delegate void CreatePlayerDataEventHandler();

    [Signal] public delegate void TransitPlayerBodyEventHandler(Node2D level, string spawnPointName);
    [Signal] public delegate void TransitPlayerBodyStartedEventHandler();
    [Signal] public delegate void TransitPlayerBodyFinishedEventHandler(Player playerBody);


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
        EmitSignal(SignalName.TransitPlayerBodyStarted);

        _playerBody?.QueueFree();
        _playerBody = (Player)PlayerBodyScene.Instantiate();
        _playerBody.Position = level.GetNode<Marker2D>(spawnPointName).Position;
        GetTree().Root.GetNode<Node2D>("Main/World").AddChild(_playerBody);

        _playerBody.Initialize(_playerData);

        EmitSignal(SignalName.TransitPlayerBodyFinished, _playerBody);
    }
}
