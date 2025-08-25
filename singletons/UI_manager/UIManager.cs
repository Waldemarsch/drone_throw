using Godot;
using System;
using Godot.Collections;

public partial class UIManager : Node
{

    public static UIManager Instance;

    private CanvasLayer UICanvasLayer;

    [Signal] public delegate void AddUIElementEventHandler(Control uiElement);
    [Signal] public delegate void ShowUIElementEventHandler(string uiElementName);
    [Signal] public delegate void HideUIElementEventHandler(string uiElementName);

    private Dictionary _UiElementsList = [];

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        UICanvasLayer = GetTree().Root.GetNode<CanvasLayer>("Ui");

        AddUIElement += OnAddUIElement;
        ShowUIElement += OnShowUIElement;
        HideUIElement += OnHideUIElement;
    }


    private void OnAddUIElement(Control uiElement)
    {
        _UiElementsList[uiElement.Name] = uiElement;
        uiElement.Hide();
        uiElement.ProcessMode = ProcessModeEnum.Disabled;
        UICanvasLayer.AddChild(uiElement);
    }

    private void OnShowUIElement(string uiElementName)
    {
        GD.Print(_UiElementsList);
        var uiElement = (Control)_UiElementsList[uiElementName];
        uiElement.Show();
        uiElement.ProcessMode = ProcessModeEnum.Pausable;
    }

    private void OnHideUIElement(string uiElementName)
    {
        var uiElement = (Control)_UiElementsList[uiElementName];
        uiElement.Hide();
        uiElement.ProcessMode = ProcessModeEnum.Disabled;
    }
}
