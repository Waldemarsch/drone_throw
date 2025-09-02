using Godot;
using System;

public partial class Main : Node
{
    private LevelContainer _levelContainer;
    private UIContainer _UIContainer;

    public override void _Ready()
    {
        _levelContainer = GetNode<LevelContainer>("World/LevelContainer");
        _UIContainer = GetNode<UIContainer>("UIContainer");

        CallDeferred(nameof(PreloadGame));
    }
    
    private void PreloadGame()
    {
        Godot.Collections.Array scenesOnPreload = [];
        scenesOnPreload.Add(_levelContainer.levelContainerResource.Paths["MainWorld"]);
        scenesOnPreload.Add(_UIContainer.UIContainerResource.Paths["MainMenu"]);
        scenesOnPreload.Add(_UIContainer.UIContainerResource.Paths["GameInterface"]);
        scenesOnPreload.Add(_UIContainer.UIContainerResource.Paths["UpgradeMenu"]);

        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Load, scenesOnPreload);

        SceneManager.Instance.LoadingCompleted += () =>
        {
            UIManager.Instance.EmitSignal(UIManager.SignalName.EnableUIElement, "MainMenu");
        };
    }

}
