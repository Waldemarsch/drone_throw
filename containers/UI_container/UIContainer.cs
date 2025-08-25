using Godot;
using System;

public partial class UIContainer : CanvasLayer
{
    [Export] public ContainerResource UIContainerResource;

    [Export] public Godot.Collections.Array<Control> loadedUIScenes = [];
}
