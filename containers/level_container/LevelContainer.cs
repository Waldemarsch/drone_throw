using Godot;
using System;

public partial class LevelContainer : Node
{
    [Export] public ContainerResource levelContainerResource;

    [Export] public Godot.Collections.Array<Node2D> loadedLevelScenes = [];

}
