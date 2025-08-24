using Godot;
using System;

public partial class GameManager : Node
{

    public static GameManager Instance { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        Instance = this;
    }
    
}
