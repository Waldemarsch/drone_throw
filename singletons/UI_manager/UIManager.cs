using Godot;
using System;
using Godot.Collections;

public partial class UIManager : Node
{

    public static UIManager Instance;

    [Export] private UIContainer _UIContainer;

    [Signal] public delegate void LoadUIElementEventHandler(Control uiElement);
    [Signal] public delegate void AddUIElementEventHandler(string uiElementName);
    [Signal] public delegate void ShowUIElementEventHandler(string uiElementName);
    [Signal] public delegate void HideUIElementEventHandler(string uiElementName);

    private Dictionary _UiElementsList = [];

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _UIContainer = GetTree().Root.GetNode<UIContainer>("Main/UIContainer");

        LoadUIElement += OnLoadUIElement;
        AddUIElement += OnAddUIElement;
        ShowUIElement += OnShowUIElement;
        HideUIElement += OnHideUIElement;
    }

    private void OnLoadUIElement(Control uiElement)
    {
        _UIContainer.loadedUIScenes.Add(uiElement);
    }

    private void OnAddUIElement(string uiElementName)
    {
        Control uiElement = null;
        foreach (Control loadedUIScene in _UIContainer.loadedUIScenes) GD.Print(loadedUIScene.Name);
        foreach (Control loadedUIScene in _UIContainer.loadedUIScenes)
        {
            if (loadedUIScene.Name == uiElementName)
            {
                uiElement = (Control)loadedUIScene.Duplicate();
                break;
            }
        }
        if (uiElement == null) return;
        
        _UiElementsList[uiElementName] = uiElement;
        uiElement.Hide();
        uiElement.ProcessMode = ProcessModeEnum.Disabled;
        _UIContainer.AddChild(uiElement);
    }

    private void OnShowUIElement(string uiElementName)
    {
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
