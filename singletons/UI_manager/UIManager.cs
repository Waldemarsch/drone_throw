using Godot;
using System;

public partial class UIManager : Node
{

    public static UIManager Instance;

    private CanvasLayer UICanvasLayer;

    [Signal] public delegate void AddUIElementEventHandler(Control uiElement);

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        UICanvasLayer = GetTree().Root.GetNode<CanvasLayer>("Ui");

        AddUIElement += OnAddUIElement;
    }


    private void OnAddUIElement(Control uiElement)
    {
        uiElement.Hide();
        uiElement.ProcessMode = ProcessModeEnum.Disabled;
        UICanvasLayer.AddChild(uiElement);
    }
}
