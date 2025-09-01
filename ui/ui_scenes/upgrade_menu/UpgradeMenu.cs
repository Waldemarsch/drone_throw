using Godot;
using System;

public partial class UpgradeMenu : Control
{
    [Export] public StyleBoxFlat BoughtUpgradeStyleBox;
    [Export] public StyleBoxFlat UnboughtUpgradeStyleBox;

    private TextureButton _playButton;

    private VBoxContainer _upgradeLevelContainer;

    public override void _Ready()
    {
        base._Ready();

        _playButton = GetNode<TextureButton>("PanelContainer/PlayButton");
        _playButton.Pressed += OnPlayButtonPressed;

        foreach (var upgradeLevel in GetNode<VBoxContainer>("PanelContainer/HBoxContainer/UpgradeLevels").GetChildren())
        {
            int intUpgradeLevel = 0;
            if (upgradeLevel.Name == "GeneralUpgradeBox") intUpgradeLevel = PlayerManager.Instance._playerData.GeneralUpgradeLevel;
            else if (upgradeLevel.Name == "EngineUpgradeBox") intUpgradeLevel = PlayerManager.Instance._playerData.EngineUpgradeLevel;
            else if (upgradeLevel.Name == "GunUpgradeBox") intUpgradeLevel = PlayerManager.Instance._playerData.GunUpgradeLevel;
            else if (upgradeLevel.Name == "GearUpgradeBox") intUpgradeLevel = PlayerManager.Instance._playerData.GearUpgradeLevel;
            else if (upgradeLevel.Name == "ShieldUpgradeBox") intUpgradeLevel = PlayerManager.Instance._playerData.ShieldUpgradeLevel;

            var panelContainer = upgradeLevel.GetNode<HBoxContainer>("HBoxContainer");

            for (var i = 0; i < intUpgradeLevel; i++)
            {
                panelContainer.GetChild<PanelContainer>(i).AddThemeStyleboxOverride("panel", BoughtUpgradeStyleBox);
            }
        }



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
