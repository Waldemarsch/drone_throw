using Godot;
using System;

public partial class UpgradeMenu : Control
{
    private TextureButton _playButton;

    public override void _Ready()
    {
        base._Ready();

        _playButton = GetNode<TextureButton>("PanelContainer/PlayButton");
        _playButton.Pressed += OnPlayButtonPressed;

        // foreach (HBoxContainer upgradeBox in GetNode<GridContainer>("PanelContainer/VBoxContainer/UpgradeGrid").GetChildren())
        // {
        //     if (upgradeBox.Name == "GeneralUpgradeBox")
        //         {
        //             for (var i = 0; i < PlayerManager.Instance._playerData.GeneralUpgradeLevel; i++)
        //             {
        //                 (Panel)upgradeBox.GetNode<HBoxContainer>("MarginContainer/HBoxContainer").GetChildren()[i].T
        //             }
        //         }
        //     foreach (Control upgradeScale in upgradeBox.GetNode<HBoxContainer>("MarginContainer/HBoxContainer").GetChildren())
        //     {

        //     }
        // }
    }

    private async void OnPlayButtonPressed()
    {
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.Change);

        await ToSignal(SceneManager.Instance, SceneManager.SignalName.AllowSceneTransition);

        UIManager.Instance.EmitSignal(UIManager.SignalName.HideUIElement, "UpgradeMenu");
        UIManager.Instance.EmitSignal(UIManager.SignalName.AddUIElement, "GameInterface");
        UIManager.Instance.EmitSignal(UIManager.SignalName.ShowUIElement, "GameInterface");

        LevelManager.Instance.EmitSignal(LevelManager.SignalName.ChangeLevel, "MainWorld", "DefaultSpawn");

        QueueFree();
    }

}
