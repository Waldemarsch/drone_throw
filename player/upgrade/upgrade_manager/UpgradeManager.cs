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

    private Player _player;

    private Node2D _generalUpgradeScene;
    private Node2D _engineUpgradeScene;
    private Node2D _gunUpgradeScene;
    private Node2D _gearUpgradeScene;
    private Node2D _shieldUpgradeScene;

    public override void _Ready()
    {
        _player = GetOwner<Player>();

        _player.InitializeComponents += Initialize;
    }

    public void Initialize()
    {
        ApplyUpgrade(EUpgradeType.General);
    }

    private void ApplyUpgrade(EUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case EUpgradeType.General:
                _generalUpgradeScene?.QueueFree();
                _generalUpgradeScene = GeneralUpgradeScenes[_player.PlayerDataResource.GeneralUpgradeLevel-1].Instantiate<Node2D>();
                _player.AddChild(_generalUpgradeScene);

                if (_engineUpgradeScene != null)
                    _engineUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("EngineSocket").Position;
                if (_engineUpgradeScene != null)
                    _gunUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("GunSocket").Position;
                if (_engineUpgradeScene != null)
                    _gearUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("GearSocket").Position;
                if (_engineUpgradeScene != null)
                    _shieldUpgradeScene.Position = _generalUpgradeScene.GetNode<Marker2D>("ShieldSocket").Position;
                break;

            case EUpgradeType.Engine:
                _engineUpgradeScene?.QueueFree();
                _engineUpgradeScene = EngineUpgradeScenes[_player.PlayerDataResource.GeneralUpgradeLevel-1].Instantiate<Node2D>();
                _engineUpgradeScene.Position =  _generalUpgradeScene.GetNode<Marker2D>("EngineSocket").Position;
                _player.AddChild(_engineUpgradeScene);
                break;

            case EUpgradeType.Gun:
                _gunUpgradeScene?.QueueFree();
                _gunUpgradeScene = GunUpgradeScenes[_player.PlayerDataResource.GunUpgradeLevel-1].Instantiate<Node2D>();
                _gunUpgradeScene.Position =  _generalUpgradeScene.GetNode<Marker2D>("GunSocket").Position;
                _player.AddChild(_gunUpgradeScene);
                break;

            case EUpgradeType.Gear:
                _gearUpgradeScene?.QueueFree();
                _gearUpgradeScene = GearUpgradeScenes[_player.PlayerDataResource.GearUpgradeLevel-1].Instantiate<Node2D>();
                _gearUpgradeScene.Position =  _generalUpgradeScene.GetNode<Marker2D>("GearSocket").Position;
                _player.AddChild(_gearUpgradeScene);
                break;

            case EUpgradeType.Shield:
                _shieldUpgradeScene?.QueueFree();
                _shieldUpgradeScene = ShieldUpgradeScenes[_player.PlayerDataResource.ShieldUpgradeLevel-1].Instantiate<Node2D>();
                _shieldUpgradeScene.Position =  _generalUpgradeScene.GetNode<Marker2D>("ShieldSocket").Position;
                _player.AddChild(_shieldUpgradeScene);
                break;
        }
    }
}
