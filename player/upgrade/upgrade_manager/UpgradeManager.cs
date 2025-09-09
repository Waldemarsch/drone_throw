using Godot;
using System;
using Godot.Collections;

public partial class UpgradeManager : Node
{
    [Export] public Array<PackedScene> GeneralUpgradeScenes;
    [Export] public Array<PackedScene> EngineUpgradeScenes;
    [Export] public Array<PackedScene> GunUpgradeScenes;
    [Export] public Array<PackedScene> GearUpgradeScenes;
    [Export] public Array<PackedScene> ShieldUpgradeScenes;

    private PlayerBody _playerBody;

    private Node2D _generalUpgradeScene;
    private Node2D _engineUpgradeScene;
    private Node2D _gunUpgradeScene;
    private Node2D _gearUpgradeScene;
    private Node2D _shieldUpgradeScene;

    public override void _Ready()
    {
        _playerBody = GetOwner<PlayerBody>();

        _playerBody.InitializeBodyComponents += OnInitializeBodyComponents;
    }

    public void OnInitializeBodyComponents()
    {
        ApplyUpgrade(EUpgradeType.General);

        var playerData = PlayerManager.Instance.PlayerDataResource;

        if (playerData.EngineUpgrade.CurrentUpgradeLevel > 0) ApplyUpgrade(EUpgradeType.Engine);
        if (playerData.GunUpgrade.CurrentUpgradeLevel > 0) ApplyUpgrade(EUpgradeType.Gun);
        if (playerData.GearUpgrade.CurrentUpgradeLevel > 0) ApplyUpgrade(EUpgradeType.Gear);
        if (playerData.ShieldUpgrade.CurrentUpgradeLevel > 0) ApplyUpgrade(EUpgradeType.Shield);
    }

    public Node2D GetUpgradeScene(EUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case EUpgradeType.General:
                return _generalUpgradeScene;
            case EUpgradeType.Engine:
                return _engineUpgradeScene;
            case EUpgradeType.Gun:
                return _gunUpgradeScene;
            case EUpgradeType.Gear:
                return _gearUpgradeScene;
            case EUpgradeType.Shield:
                return _shieldUpgradeScene;

            default:
                return null;
        }
    }

    private void ApplyUpgrade(EUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case EUpgradeType.General:
                _generalUpgradeScene?.QueueFree();
                _generalUpgradeScene = GeneralUpgradeScenes[_playerBody.PlayerDataResource.GeneralUpgrade.CurrentUpgradeLevel - 1].Instantiate<Node2D>();
                _playerBody.AddChild(_generalUpgradeScene);
                GD.Print(_generalUpgradeScene);
                break;

            case EUpgradeType.Engine:
                GD.Print(_generalUpgradeScene);
                _engineUpgradeScene?.QueueFree();
                _engineUpgradeScene = EngineUpgradeScenes[_playerBody.PlayerDataResource.GeneralUpgrade.CurrentUpgradeLevel - 1].Instantiate<Node2D>();
                _engineUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("EngineSocket").Position;
                _playerBody.AddChild(_engineUpgradeScene);
                break;

            case EUpgradeType.Gun:
                _gunUpgradeScene?.QueueFree();
                _gunUpgradeScene = GunUpgradeScenes[_playerBody.PlayerDataResource.GunUpgrade.CurrentUpgradeLevel - 1].Instantiate<Node2D>();
                _gunUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("GunSocket").Position;
                _playerBody.AddChild(_gunUpgradeScene);
                break;

            case EUpgradeType.Gear:
                _gearUpgradeScene?.QueueFree();
                _gearUpgradeScene = GearUpgradeScenes[_playerBody.PlayerDataResource.GearUpgrade.CurrentUpgradeLevel - 1].Instantiate<Node2D>();
                _gearUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("GearSocket").Position;
                _playerBody.AddChild(_gearUpgradeScene);
                break;

            case EUpgradeType.Shield:
                _shieldUpgradeScene?.QueueFree();
                _shieldUpgradeScene = ShieldUpgradeScenes[_playerBody.PlayerDataResource.ShieldUpgrade.CurrentUpgradeLevel - 1].Instantiate<Node2D>();
                _shieldUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("ShieldSocket").Position;
                _playerBody.AddChild(_shieldUpgradeScene);
                break;
        }
    }
}
