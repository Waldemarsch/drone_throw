using Godot;
using System;

public partial class Player : Node2D
{
    public PlayerData PlayerDataResource { get; private set; }

    [Signal] public delegate void InitializePlayerEventHandler();

    private PlayerBody _playerBody;

    public override void _Ready()
    {
        base._Ready();

        _playerBody = GetNode<PlayerBody>("PlayerBody");
    }


    public void Initialize(PlayerData playerData)
    {
        PlayerDataResource = playerData;

        EmitSignal(SignalName.InitializePlayer);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        this.GlobalPosition = _playerBody.GlobalPosition;
    }


}
