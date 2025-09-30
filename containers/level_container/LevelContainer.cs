using Godot;
using System;

public partial class LevelContainer : Node2D
{
    [Export] public ContainerResource levelContainerResource;

    public Node2D loadedLevel;

}
