using Godot;
using System;
using Godot.Collections;
using System.Linq;
using System.Collections.Generic;

public partial class UIManager : Node
{

    public static UIManager Instance;

    [Export] private UIContainer _UIContainer;

    [Signal] public delegate void LoadUIElementEventHandler(Control uiElement);
    [Signal] public delegate void EnableUIElementEventHandler(string uiElementName);
    [Signal] public delegate void HideUIElementEventHandler(string uiElementName);

    private Dictionary _UiElementsList = [];

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _UIContainer = GetTree().Root.GetNode<UIContainer>("Main/UIContainer");

        LoadUIElement += OnLoadUIElement;
        EnableUIElement += OnEnableUIElement;
        HideUIElement += OnHideUIElement;
    }

    private void OnLoadUIElement(Control uiElement)
    {
        _UIContainer.loadedUIScenes.Add(uiElement);
    }

    private void OnEnableUIElement(string uiElementName)
    {
        var uiElement = (Control)_UiElementsList.GetValueOrDefault(uiElementName);

        if (uiElement != null)
        {
            uiElement.Show();
            uiElement.ProcessMode = ProcessModeEnum.Pausable;
        }

        else
        {
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
            _UIContainer.AddChild(uiElement);
        }
        
    }

    private void OnHideUIElement(string uiElementName)
    {
        var uiElement = (Control)_UiElementsList[uiElementName];
        uiElement.Hide();
    }
}
